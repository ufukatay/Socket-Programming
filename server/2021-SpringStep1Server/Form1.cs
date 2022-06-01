using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2021_SpringStep1Server
{
    public partial class Form1 : Form
    {
        int lineCount = File.ReadLines(@"../../user-db.txt").Count();

        Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        List<Socket> clientSockets = new List<Socket>();
        List<string> clientusernames = new List<string>();

        int postCount = CountPost();

        bool terminating = false;
        bool listening = false;
        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            InitializeComponent();
        }

        private void button_listen_Click(object sender, EventArgs e)
        {
            int port;
            if (Int32.TryParse(textBox_port.Text, out port))
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, port); //listen in any interface, initialize end point here. 
                serverSocket.Bind(endPoint);
                serverSocket.Listen(3); 

                listening = true;
                button_listen.Enabled = false;

                //When client disconnect no problem in the server so no need to check here with try. 
                Thread acceptThread = new Thread(Accept); // Thread to accept new clients from now on. 
                acceptThread.Start();

                serverLogs.AppendText("Started listening on port: " + port + "\n");

            }
            else
            {
                serverLogs.AppendText("Please check port number \n");
            }

        }

        private void Accept() //Accepting new clients to the server. 
        {

            while (listening)
            {
                try
                {
                    Socket newClient = serverSocket.Accept(); // accept corresponding sockets for clients.

                    // Need to check username whether from database or not.
                    Thread usernameCheckThread = new Thread(() => usernameCheck(newClient));
                    usernameCheckThread.Start();
                }
                catch
                {
                    if (terminating) // If we close the server. No crash, correctly closed and not listening from now on. 
                    {
                        listening = false;
                    }
                    else //Problem occured here. 
                    {
                        serverLogs.AppendText("The socket stopped working.\n");
                    }

                }
            }

        }

        private void usernameCheck(Socket thisClient)
        {
            string message = "NOT_FOUND"; // will be used for usernames from outside the database. 
            try
            {
                Byte[] username_buffer = new Byte[64];
                thisClient.Receive(username_buffer);

                string username = Encoding.Default.GetString(username_buffer); // Convert byte array to string.
                username = username.Substring(0, username.IndexOf("\0"));
                //username = username.Trim('\0');

                if (clientusernames.Contains(username)) // if in database but already connected.
                {
                    serverLogs.AppendText(username + " has tried to connect from another client!\n");
                    message = "Already_Connected";
                }
                else
                {
                    var lines = File.ReadLines(@"../../user-db.txt"); // check the txt line by line.
                    foreach (var line in lines)
                    {
                        if (line == username) // if the db contains the username, can connect !
                        {
                            clientSockets.Add(thisClient);
                            clientusernames.Add(username);
                            message = "SUCCESS";
                            serverLogs.AppendText(username + " has connected.\n");
                            
                            //After the client is connected, Received information from the client's actions.
                            Thread receiveThread = new Thread(() => Receive(thisClient, username)); //Receive posts.
                            receiveThread.Start();
                        }
                    }

                }
                if(message=="NOT_FOUND")
                {
                    serverLogs.AppendText(username + " tried to connect to the server but cannot!\n");
                }
                try
                {
                    thisClient.Send(Encoding.Default.GetBytes(message)); //send the corresponding message to the client.
                }
                catch
                {
                    serverLogs.AppendText("There was a problem when sending the username response to the client.\n");
                }
            }

            catch
            {
                serverLogs.AppendText("Problem receiving username.\n");
            }


        }

        private void Receive(Socket thisClient, string username)//Actions from clients.
        {
            bool connected = true; //To receive information, should be connected by default.
            while (connected && !terminating) //still connected and not closing.
            {
                try
                {
                    Byte[] buffer = new Byte[64];
                    thisClient.Receive(buffer);//Gets information related to thisclient.

                    string incomingMessage = Encoding.Default.GetString(buffer);//convert byte array to string.
                    //incomingMessage = incomingMessage.Substring(0, incomingMessage.IndexOf("\0"));
                    incomingMessage = incomingMessage.Trim('\0');

                    //string label = incomingMessage.Substring(0, 10);

                    if (incomingMessage.Substring(0, 10) == "DISCONNECT")
                    {
                        thisClient.Close();
                        clientSockets.Remove(thisClient);
                        clientusernames.Remove(username);//remove it from the connected list.
                        connected = false;
                        serverLogs.AppendText(username + " has disconnected\n");
                    }
                    else if (incomingMessage.Substring(0, 10) == "SHOW_POSTS")
                    {
                        allposts(thisClient, username); //This function will print all posts when requested. 
                    }
                    else if(incomingMessage.Substring(0, 10) == "MY_POSTSSS")
                    {
                        myposts(thisClient, username);
                    }

                    else if (incomingMessage.Substring(0, 10) == "FRIENDPOST")
                    {
                        friendposts(thisClient, username);
                    }

                    else if (incomingMessage.Substring(0, 10) == "SEND_POSTS")
                    {
                        string post = incomingMessage.Substring(10);
                        postCount += 1;
                        postToLog(username, postCount, post);
                    }

                    else if (incomingMessage.Substring(0, 10) == "DELETEPOST")
                    {
                        string id = incomingMessage.Substring(10);
                        deleteFromLog(thisClient, username, id);
                    }
                    
                    else if (incomingMessage.Substring(0, 10) == "ADD_FRIEND")
                    {
                        string friend = incomingMessage.Substring(10);
                        addFriend(thisClient, username, friend);
                    }
                    else if (incomingMessage.Substring(0, 10) == "REMOVEFRND")
                    {
                        removeFriend(thisClient, username);
                    }

                }
                catch
                {
                    if (!terminating)
                    {
                        serverLogs.AppendText(username + " has disconnected.\n");
                    }
                    thisClient.Close();
                    clientSockets.Remove(thisClient);
                    clientusernames.Remove(username);
                    connected = false;
                }
            }

        }

        private void postToLog(string username, object postID, string post)
        {
            DateTime currentDateTime = DateTime.Now;
            string DT = currentDateTime.ToString("s"); // 2021-11-20T16:54:52
            using (StreamWriter file = new StreamWriter("../../posts.log", append: true))//append all posts to a file.
            {
                file.WriteLine(DT + " /" + username + "/" + postID.ToString() + "/" + post + "/");
            }
            serverLogs.AppendText(username + " has sent a post:\n" + post + "\n");
        }

        private void addFriend(Socket thisClient, string username, string friend)
        {
            bool exist = false;
            var lines = File.ReadLines(@"../../user-db.txt"); // check the txt line by line.
            foreach (var line in lines)
            {
                if (line == friend) // if the db contains the username, can connect !
                {
                    exist = true;
                }
            }

            if (username == friend)
            {
                Byte[] buffer1 = Encoding.Default.GetBytes("ADD_FRIENDYou cannot add yourself as a friend!");
                thisClient.Send(buffer1);
                serverLogs.AppendText(username + " has tried to add itself but it couldn't!\n");
            }

            else if (!exist)
            {
                Byte[] buffer2 = Encoding.Default.GetBytes("ADD_FRIENDYou cannot add " + friend + " because it is not a db user!");
                thisClient.Send(buffer2);
                serverLogs.AppendText(username + " has tried to add " + friend + " as a friend but it is not a db user so it couldn't be added!\n");
            }

            else
            {
                using (StreamWriter file = new StreamWriter("../../friends-db.txt", append: true))//append all posts to a file.
                {
                    file.WriteLine(username + "-" + friend);
                    file.WriteLine(friend + "-" + username);
                }
                Byte[] buffer3 = Encoding.Default.GetBytes("ADD_FRIENDYou have added " + friend + " as a friend!");
                thisClient.Send(buffer3);
                serverLogs.AppendText(username + " has added " + friend + " as a friend!\n");                
            }
        }

        private void deleteFromLog(Socket thisClient, string username, string id)
        {
            string nid = "/" + id + "/";
            bool exist = false;
            bool isMine = true;

            string tempFile = Path.GetTempFileName();
            using (var sr = new StreamReader("../../posts.log"))
            using (var sw = new StreamWriter(tempFile))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    if (!(line.Contains(nid)))
                    {
                        sw.WriteLine(line);
                    }
                    else if (line.Contains(nid) && !line.Contains(username))
                    {
                        sw.WriteLine(line);
                        isMine = false;
                    }
                    else
                    {
                        exist = true;
                    }
                }
            }

            File.Delete("../../posts.log");
            File.Move(tempFile, "../../posts.log");

            if (exist)
            {
                Byte[] buffer1 = Encoding.Default.GetBytes("DELETEPOSTYou have deleted the post with ID: " + id);
                thisClient.Send(buffer1);
                serverLogs.AppendText(username + " has deleted the post with ID: " + id + "\n");
            }

            else if (!isMine)
            {
                Byte[] buffer2 = Encoding.Default.GetBytes("DELETEPOSTPost with ID: " + id + " is not yours!");
                thisClient.Send(buffer2);
                serverLogs.AppendText(username + " has tried to delete post with ID " + id + " which is not its post!\n");
            }

            else if (!exist)
            {
                Byte[] buffer3 = Encoding.Default.GetBytes("DELETEPOSTThere is no post with ID: " + id);
                thisClient.Send(buffer3);
                serverLogs.AppendText(username + " has tried to delete post with ID " + id + " which is not exist!\n");
            }

        }

        private void allposts(Socket thisClient, string username)
        {
            string allposts = File.ReadAllText(@"../../posts.log");
            string pattern = @"\d\d\d\d[-]\d\d[-]\d\d[T]\d\d[:]\d\d[:]\d\d";

            Regex regex = new Regex(pattern);
            string[] splitted = regex.Split(allposts);
            MatchCollection matches = Regex.Matches(allposts, pattern);
            for (int i = 1; i < splitted.Length; i++)
            {
                int beforeid = splitted[i].IndexOf("/", 2);
                int afterid = splitted[i].IndexOf("/", beforeid + 1);
                string Name = splitted[i].Substring(2, beforeid - 2);
                string pID = splitted[i].Substring(beforeid + 1, afterid - beforeid - 1);
                string post = splitted[i].Substring(afterid + 1, splitted[i].Length - 4 - afterid);
                if (username != Name)
                {
                    try
                    {
                        Byte[] buffer1 = Encoding.Default.GetBytes("SHOW_POSTSUsername: " + Name);
                        try
                        {
                            thisClient.Send(buffer1);
                            Byte[] response = new Byte[64];
                            thisClient.Receive(response);
                            string received = Encoding.Default.GetString(response);
                            Byte[] buffer2 = Encoding.Default.GetBytes("SHOW_POSTSPostID: " + pID);
                            try
                            {
                                thisClient.Send(buffer2);
                                Byte[] response2 = new Byte[64];
                                thisClient.Receive(response);
                                string received2 = Encoding.Default.GetString(response);
                                Byte[] buffer3 = Encoding.Default.GetBytes("SHOW_POSTSPost: " + post);
                                try
                                {
                                    thisClient.Send(buffer3);
                                    Byte[] response3 = new Byte[64];
                                    thisClient.Receive(response);
                                    string received3 = Encoding.Default.GetString(response);
                                    Byte[] buffer4 = Encoding.Default.GetBytes("SHOW_POSTSTime: " + matches[i - 1] + "\n");
                                    try
                                    {
                                        thisClient.Send(buffer4);
                                        Byte[] response4 = new Byte[64];
                                        thisClient.Receive(response);
                                        string received4 = Encoding.Default.GetString(response);
                                    }
                                    catch
                                    {
                                        serverLogs.AppendText("There was a problem sending the time.\n");
                                    }
                                }
                                catch
                                {
                                    serverLogs.AppendText("There was a problem sending the post.\n");
                                }
                            }
                            catch
                            {
                                serverLogs.AppendText("There was a problem sending the post ID.\n");
                            }

                        }
                        catch
                        {
                            serverLogs.AppendText("There was a problem sending the username.\n");
                        }

                    }
                    catch
                    {
                        serverLogs.AppendText("There was a problem with the GetBytes function.\n");
                    }
                }
            }
            serverLogs.AppendText("Showed all posts for " + username + ".\n");
        }

        private void friendposts(Socket thisClient, string username)
        {
            List<string> friendList = new List<string>();
            var lines = File.ReadLines(@"../../friends-db.txt"); // check the txt line by line.
            foreach (var line in lines)
            {
                if (line.Substring(0, line.IndexOf("-")) == username)
                {
                    string dost = line.Substring(line.IndexOf("-")+1);
                    friendList.Add(dost);
                }
            }


            foreach (var konka in friendList)
            {
                var layns = File.ReadLines(@"../../posts.log"); // check the txt line by line.
                string allposts = File.ReadAllText(@"../../posts.log");
                string pattern = @"\d\d\d\d[-]\d\d[-]\d\d[T]\d\d[:]\d\d[:]\d\d";

                Regex regex = new Regex(pattern);
                string[] splitted = regex.Split(allposts);
                MatchCollection matches = Regex.Matches(allposts, pattern);
                for (int i = 1; i < splitted.Length; i++)
                {
                    int beforeid = splitted[i].IndexOf("/", 2);
                    int afterid = splitted[i].IndexOf("/", beforeid + 1);
                    string Name = splitted[i].Substring(2, beforeid - 2);
                    string pID = splitted[i].Substring(beforeid + 1, afterid - beforeid - 1);
                    string post = splitted[i].Substring(afterid + 1, splitted[i].Length - 4 - afterid);
                    if (konka == Name)
                    {
                        try
                        {
                            Byte[] buffer1 = Encoding.Default.GetBytes("FRIENDPOSTUsername: " + Name);
                            try
                            {
                                thisClient.Send(buffer1);
                                Byte[] response = new Byte[64];
                                thisClient.Receive(response);
                                string received = Encoding.Default.GetString(response);
                                Byte[] buffer2 = Encoding.Default.GetBytes("FRIENDPOSTPostID: " + pID);
                                try
                                {
                                    thisClient.Send(buffer2);
                                    Byte[] response2 = new Byte[64];
                                    thisClient.Receive(response);
                                    string received2 = Encoding.Default.GetString(response);
                                    Byte[] buffer3 = Encoding.Default.GetBytes("FRIENDPOSTPost: " + post);
                                    try
                                    {
                                        thisClient.Send(buffer3);
                                        Byte[] response3 = new Byte[64];
                                        thisClient.Receive(response);
                                        string received3 = Encoding.Default.GetString(response);
                                        Byte[] buffer4 = Encoding.Default.GetBytes("FRIENDPOSTTime: " + matches[i - 1] + "\n");
                                        try
                                        {
                                            thisClient.Send(buffer4);
                                            Byte[] response4 = new Byte[64];
                                            thisClient.Receive(response);
                                            string received4 = Encoding.Default.GetString(response);
                                        }
                                        catch
                                        {
                                            serverLogs.AppendText("There was a problem sending the time.\n");
                                        }
                                    }
                                    catch
                                    {
                                        serverLogs.AppendText("There was a problem sending the post.\n");
                                    }
                                }
                                catch
                                {
                                    serverLogs.AppendText("There was a problem sending the post ID.\n");
                                }

                            }
                            catch
                            {
                                serverLogs.AppendText("There was a problem sending the username.\n");
                            }

                        }
                        catch
                        {
                            serverLogs.AppendText("There was a problem with the GetBytes function.\n");
                        }
                    }
                }
            }
            serverLogs.AppendText("Showed all posts of friends for " + username + ".\n");
        }

        private void myposts(Socket thisClient, string username)
        {
            string allposts = File.ReadAllText(@"../../posts.log");
            string pattern = @"\d\d\d\d[-]\d\d[-]\d\d[T]\d\d[:]\d\d[:]\d\d";

            Regex regex = new Regex(pattern);
            string[] splitted = regex.Split(allposts);
            MatchCollection matches = Regex.Matches(allposts, pattern);
            for (int i = 1; i < splitted.Length; i++)
            {
                int beforeid = splitted[i].IndexOf("/", 2);
                int afterid = splitted[i].IndexOf("/", beforeid + 1);
                string Name = splitted[i].Substring(2, beforeid - 2);
                string pID = splitted[i].Substring(beforeid + 1, afterid - beforeid - 1);
                string post = splitted[i].Substring(afterid + 1, splitted[i].Length - 4 - afterid);
                if (username == Name)
                {
                    try
                    {
                        Byte[] buffer1 = Encoding.Default.GetBytes("MY_POSTSSSUsername: " + Name);
                        try
                        {
                            thisClient.Send(buffer1);
                            Byte[] response = new Byte[64];
                            thisClient.Receive(response);
                            string received = Encoding.Default.GetString(response);
                            Byte[] buffer2 = Encoding.Default.GetBytes("MY_POSTSSSPostID: " + pID);
                            try
                            {
                                thisClient.Send(buffer2);
                                Byte[] response2 = new Byte[64];
                                thisClient.Receive(response);
                                string received2 = Encoding.Default.GetString(response);
                                Byte[] buffer3 = Encoding.Default.GetBytes("MY_POSTSSSPost: " + post);
                                try
                                {
                                    thisClient.Send(buffer3);
                                    Byte[] response3 = new Byte[64];
                                    thisClient.Receive(response);
                                    string received3 = Encoding.Default.GetString(response);
                                    Byte[] buffer4 = Encoding.Default.GetBytes("MY_POSTSSSTime: " + matches[i - 1] + "\n");
                                    try
                                    {
                                        thisClient.Send(buffer4);
                                        Byte[] response4 = new Byte[64];
                                        thisClient.Receive(response);
                                        string received4 = Encoding.Default.GetString(response);
                                    }
                                    catch
                                    {
                                        serverLogs.AppendText("There was a problem sending the time.\n");
                                    }
                                }
                                catch
                                {
                                    serverLogs.AppendText("There was a problem sending the post.\n");
                                }
                            }
                            catch
                            {
                                serverLogs.AppendText("There was a problem sending the post ID.\n");
                            }

                        }
                        catch
                        {
                            serverLogs.AppendText("There was a problem sending the username.\n");
                        }

                    }
                    catch
                    {
                        serverLogs.AppendText("There was a problem with the GetBytes function.\n");
                    }
                }
            }
            serverLogs.AppendText("Showed all posts for " + username + ".\n");
        }

        private void removeFriend (Socket thisClient, string username)
        {
            var lines = File.ReadLines(@"../../friends-db.txt"); // check the txt line by line.
            string dost = "";

            foreach (var layn in lines)
            {
                if (layn.Substring(0, layn.IndexOf("-")) == username)
                {
                    dost = layn.Substring(layn.IndexOf("-") + 1);
                }
            }

            string tempFile = Path.GetTempFileName();

            using (var sr = new StreamReader("../../friends-db.txt"))
            using (var sw = new StreamWriter(tempFile))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    if (!(line.Contains(username) && line.Contains(dost)))
                    {
                        sw.WriteLine(line);
                    }
                }
            }

            File.Delete("../../friends-db.txt");
            File.Move(tempFile, "../../friends-db.txt");

            Byte[] buffer1 = Encoding.Default.GetBytes("REMOVEFRNDYou have removed " + dost + " from your friend list!");
            thisClient.Send(buffer1);
            serverLogs.AppendText(username + " has removed " + dost + " from friend list!\n");
        }
        
        private static int CountPost()
        {
            if (!File.Exists(@"../../posts.log"))//if not generated before.
            {
                File.Create(@"../../posts.log").Dispose();
            }

            string allPosts = File.ReadAllText(@"../../posts.log");

            if (allPosts == "")
            {
                return 0;
            }
            //maybe also line by line can be tried.
            string pattern = @"\d\d\d\d[-]\d\d[-]\d\d[T]\d\d[:]\d\d[:]\d\d";

            Regex regex = new Regex(pattern);
            string[] splitted = regex.Split(allPosts);

            int beforeID = splitted[splitted.Length - 1].IndexOf("/", 2);
            int afterID = splitted[splitted.Length - 1].IndexOf("/", beforeID + 1);

            string pID = splitted[splitted.Length - 1].Substring(beforeID + 1, afterID - beforeID - 1);

            return Int32.Parse(pID);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            listening = false;
            terminating = true;
            Environment.Exit(0);
        }

      
    }
}
