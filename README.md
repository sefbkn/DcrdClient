# DcrdClient
Client library for interacting with dcrd


# Usage


The following code demonstrates how to connect to and ping an instance of dcrd 
listening on the default testnet port, on the same host.

    var apiUrl = "http://localhost:19109";
    var credentials = new NetworkCredential("user", "pass");
    var handler = new HttpClientHandler { Credentials = credentials };
    var client = new DcrdHttpClient(apiUrl, handler);
    await client.PingAsync();

