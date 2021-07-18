# Howler

[![Open in Visual Studio Code](https://open.vscode.dev/badges/open-in-vscode.svg)](https://open.vscode.dev/HowlerChat/Howler)

Howler is a project designed for open learning of the architecture and components of a chat service like Discord. See the latest tickets on our public JIRA tracker: https://howler.atlassian.net/jira/software/c/projects/HOWL/issues/?filter=allissues

Keep track of the design prototypes: https://www.figma.com/file/OOycgY2cQPJ0SdbCbRQ0Os/Howler-App?node-id=0%3A1

Live stream development and learning sessions are hosted every Saturday at 6pm Pacific Time (unless otherwise announced) at https://twitch.tv/Swearw0lves

Join our Wolfpack! https://www.codewolfpack.com/


# Authentication

Authentication is two-tiered to facilitate federation - The official Howler auth server is used by all chat servers that wish to integrate with this flow, but all chat servers must provide a subsequent auth token for clients to use with their spaces.

Main auth server link for testing: https://auth.howler.chat/login?client_id=6b75ooll3b86ugauhu22vj39ra&response_type=token&scope=email+openid+profile&redirect_uri=http://localhost:8000

# Native Development

If you are on OS X, be sure that Xcode is installed.

If you are on Windows, be sure you run the easy installer script at https://microsoft.github.io/react-native-windows/docs/rnw-dependencies

On all of the above, run `npm install -g react-native` afterwards, and you will be good to go.
