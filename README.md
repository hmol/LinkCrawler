# LinkCrawler
Simple C# console application that will crawl the given webpage for broken image-tags and hyperlinks. The result of this will be written to output. Right now we have these outputs: console, csv, slack.


## Why?
Because it could be useful to know when a webpage you have responsibility for displays broken links to it's users. I have this running continuously, but you don't have to. For instance, after upgrading your CMS, changing database-scheme, migrating content etc, it can be relevant to know if this did or did not not introduce broken links. Just run this tool one time and you will know exactly how many links are broken, where they link to, and where they are located.

## Build
Clone repo :point_right: open solution in Visual Studio :point_right: build :facepunch:

AppVeyor is used as CI, so when code is pushed to this repo the solution will get built and all tests will be run. 

| Branch | Build status |
| :-----  | :---------------------------------------|
| develop | [![Build status](https://ci.appveyor.com/api/projects/status/syw3l7xeicy7xc0b/branch/develop?svg=true)](https://ci.appveyor.com/project/hmol/linkcrawler/branch/develop) |
| master | [![Build status](https://ci.appveyor.com/api/projects/status/syw3l7xeicy7xc0b/branch/master?svg=true)](https://ci.appveyor.com/project/hmol/linkcrawler/branch/master) |

## AppSettings

| Key     				       | Usage           					   |
| :--------------------------  | :---------------------------------------|
| ```BaseUrl   ```   				  | Base url for site to crawl  	       |
| ```SuccessHttpStatusCodes```	|	HTTP status codes that are considered "successful". Example: "1xx,2xx,302,303"	|
| ```CheckImages```      			  | If true, <img src=".." will be checked |
| ```ValidUrlRegex   ```   				  | Regex to match valid urls  	       |
| ```Slack.WebHook.Url```  | Url to the slack webhook. If empty, it will not try to send message to slack     		   |
| ```Slack.WebHook.Bot.Name``` 	  | Custom name for slack bot   		   |
| ```Slack.WebHook.Bot.IconEmoji``` | Custom Emoji for slack bot  	       |
| ```OnlyReportBrokenLinksToOutput```      			  | If true, only broken links will be reported to output. |
| ```Slack.WebHook.Bot.MessageFormat``` | String format message that will be sent to slack  	       |
| ```Csv.FilePath```   				  | File path for the CSV file  	   |
| ```Csv.Overwrite```   			  | Whether to overwrite or append (if file exists)  	       |
| ```Csv.Delimiter```   			  | Delimiter between columns in the CSV file (like ',' or ';')  	       |
| ```PrintSummary```   			  | If true, a summary will be printed when all links have been checked.  	       |
| ```FollowRedirects```           | Boolean, if true the crawler will treat pages with HTTP response codes in ```FollowCodes``` as a redirect and follow them. | 
| ```FollowCodes```               | Comma separated list of HTTP status codes to treat as a redirect. Default "301,302". | 

Ther also is a ```<outputProviders>``` that controls what output should be used.

## Output to file
```LinkCrawler.exe >> crawl.log``` will save output to file.
![Slack](http://henrikm.com/content/images/2016/Feb/as-file.png "Output to file")

## Output to slack
If configured correctly, the defined slack-webhook will be notified about broken links.
![Slack](http://henrikm.com/content/images/2016/Feb/blurred1.jpg "Slack")

##How I use it
I have it running as an Webjob in Azure, scheduled every 4 days. It will notify the slack-channel where the editors of the website dwells.

Creating a webjob is simple. Just put your compiled project files (/bin/) inside a .zip, and upload it.
![Slack](http://henrikm.com/content/images/2016/Feb/azure-webjob-setup-1.PNG "WebJob")

Schedule it.

![Slack](http://henrikm.com/content/images/2016/Feb/azure-scheduele.PNG)

The output of a webjob is available because Azure saves it in log files.
![Slack](http://henrikm.com/content/images/2016/Feb/azure-log.PNG)


Read more about Azure Webjobs: https://azure.microsoft.com/en-us/documentation/articles/web-sites-create-web-jobs/

Read more about Slack incoming webhooks: https://api.slack.com/incoming-webhooks
