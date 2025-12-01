#nullable disable

#pragma warning disable IDE0130

namespace ShootRunner
{
    public class Command
    {
        public bool   enabled = false;
        public string shortcut = ""; // CHEK FORT THIS KEYBOARD SHORTCUT

        public string action = ""; // BUILD IN ACTIONS 

        public string open = ""; // DOCCUMENT OR URL

        public string command = ""; // PROCESS
        public string parameters = ""; // PROCESS PARAMETERS 
        public string workdir = ""; // WORKDIR FOR PROCESS

        public string window = ""; // BRING WINDOW TO FRONT BY PARTIAL NAME

        public string keypress = ""; // SIMULATE KEYPRESS        
        public string process = ""; // SEND KEYPRESS TO ALL THIS PROCESSYS DEFINED BY PATH
        public string currentwindow = ""; // APPLY KEYPRESS ONLY IF THIS WINDOW IS ACTIVATED        
    }
}
