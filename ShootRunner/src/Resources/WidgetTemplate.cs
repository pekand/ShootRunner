#pragma warning disable IDE0130

namespace ShootRunner
{
    public class WidgetTemplate
    {
        public static string BasicTemplate() {
            return @"<!DOCTYPE html>
<html lang=""en"">
    <meta charset=""utf-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1, shrink-to-fit=no"">
    <meta name=""Description"" content=""description here."">
    <title>Widget</title>
    <style type=""text/css""></style>

    <main>
        <h1>Widget</h1>        
    </main>
    <script>
        function init() {

        }

        window.addEventListener(""load"", init);
    </script>
</html>";

        }
    }
}
