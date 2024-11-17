using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootRunner
{
    public class WidgetTemplate
    {
        public string BasicTemplate() {
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
