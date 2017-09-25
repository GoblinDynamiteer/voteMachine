const String web_response_string_head = R"(
HTTP/1.1 200 OK;
Content-Type: text/html

<!DOCTYPE html>
<html lang="sv">
    <head>
        <meta charset="utf-8">
        <meta name="viewport" content="width=device-width">
        <link rel="stylesheet" href="http://code.jquery.com/mobile/1.4.5/jquery.mobile-1.4.5.min.css" />
        <script src="http://code.jquery.com/jquery-1.11.1.min.js"></script>
        <script src="http://code.jquery.com/mobile/1.4.5/jquery.mobile-1.4.5.min.js"></script>
        <link rel="stylesheet" href="http://glassdragon.se/votemachine/css/theme1.css" />
    </head>
    <body>
        <div data-role="page" data-theme-"a">
            <div data-role="header">
                <h1>
                    VoteMachine
                </h1>
            </div>
            <div data-role="main" class="ui-content">
                /n
                <ul data-role="listview">
                    <li data-role="list-divider">)";

const String web_response_string_tail = R"(
                </ul>
                /n
            </div>
            <div data-role="footer">
                <h2>
                    © Dylan och Johan
                </h2>
            </div>
        </div>

    </body>
</html>)";
