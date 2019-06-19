mkdir c:\winsdktemp

$client = new-object System.Net.WebClient
$client.DownloadFile("https://go.microsoft.com/fwlink/p/?LinkId=838916", "c:\winsdktemp\winsdksetup.exe")

Start-Process "c:\winsdktemp\winsdksetup.exe" "/features + /quiet" -NoNewWindow -Wait

refreshenv