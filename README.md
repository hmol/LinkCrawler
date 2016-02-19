# LinkCrawler
Simple C# console application that will crawl the given webpage for image-tags and hyperlinks. If some of them is not working (HttpStatusCode >= 300), an message will be sent to slack. Also response status will be written to console output.

## App.Settings

| Key     				       | Usage           					   |
| :--------------------------  | :---------------------------------------|
| ```BaseUrl   ```   				  | Base url for site to crawl  	       |
| ```CheckImages```      			  | If true, <img src=".." will be checked |
| ```ValidUrlRegex   ```   				  | Regex to match valid urls  	       |
| ```Slack.WebHook.Url```  | Url to the slack webhook. If empty, it will not try to send message to slack     		   |
| ```Slack.WebHook.Bot.Name``` 	  | Custom name for slack bot   		   |
| ```Slack.WebHook.Bot.IconEmoji``` | Custom Emoji for slack bot  	       |
| ```OnlyReportBrokenLinksToOutput```      			  | If true, only broken links will be reported to output. |
| ```Slack.WebHook.Bot.MessageFormat``` | String format message that will be sent to slack  	       |
 
## Output to console
![Example run on www.github.com](http://henrikm.com/content/images/2016/Feb/linkcrawler_example.PNG "Example run on www.github.com")

## Output to file
```LinkCrawler.exe >> crawl.log``` will save output to file.
![Slack](http://henrikm.com/content/images/2016/Feb/as-file.png "Output to file")

## Output to slack
If configured correctly, the defined slack-webhook will be notified about broken links.
![Slack](http://henrikm.com/content/images/2016/Feb/blurred1.jpg "Slack")

##How I use it
I have it running as an Webjob in Azure, scheduled to two times a week. It will notify the slack-channel where the editors of the website dwells.

Creating a webjob is simple. Just put your compiled project files (/bin/) inside a .zip, and upload it.
![Slack](http://henrikm.com/content/images/2016/Feb/azure-webjob-setup-1.PNG "WebJob")

Schedule it.

![Slack](http://henrikm.com/content/images/2016/Feb/azure-scheduele.PNG)

The output of a webjob is available because Azure saves it in log files.
![Slack](http://henrikm.com/content/images/2016/Feb/azure-log.PNG)



Read more about Slack incoming webhooks: https://api.slack.com/incoming-webhooks