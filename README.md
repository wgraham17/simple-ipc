# simple-ipc
Highly reliable, simple IPC (inter-process communication) layer based on WM_COPYDATA and Windows messages.

This work is based upon https://github.com/TheCodeKing/XDMessaging.Net. I liked the simplicity and reliability of using Windows messages (we have a client app installed on hundreds of thousands of machines and WCF is not stable), but the author of this library was bundling AWSSDK as an embedded resource. I did not want the bloat of AWS (nor did I need it) and it was breaking my ILMerge. Thus, this was born.
