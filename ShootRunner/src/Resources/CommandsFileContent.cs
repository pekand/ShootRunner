namespace ShootRunner
{
    public class CommandsFileContent
    {
        public static string  GetContent() {
            return @"
<root>
<commands>

<command>
<shortcut>WIN+C</shortcut>
<enabled>1</enabled>
<action>console</action>
</command>

<command>
<shortcut>F9</shortcut>
<enabled>1</enabled>
<action>CreatePin</action>
<parameters></parameters>
</command>

<command>
<shortcut>WIN+N</shortcut>
<enabled>0</enabled>
<command>C:\Windows\System32\notepad.exe</command>
<parameters></parameters>
</command>

<command>
<shortcut>WIN+G</shortcut>
<enabled>0</enabled>
<open>https://www.google.com/</open>
</command>

<command>
<shortcut>WIN+N</shortcut>
<enabled>0</enabled>
<window>Notepad</window>
</command>

<command>
<shortcut>F10</shortcut>
<enabled>0</enabled>
<keypress>CTRL+W</keypress>
<currentwindow>Firefox</currentwindow>
</command>

<command>
<shortcut>F12</shortcut>
<enabled>0</enabled>
<keypress>CTRL+SHIFT+T</keypress>
<currentwindow>Firefox</currentwindow>
</command>

<command>
<shortcut>Scroll</shortcut>
<enabled>0</enabled>
<action>LockPc</action>
</command>

<command>
<shortcut>Pause</shortcut>
<enabled>0</enabled>
<command>shutdown.exe</command>
<parameters>/h</parameters>
</command>

</commands>
</root>

";

        }
    }
}
