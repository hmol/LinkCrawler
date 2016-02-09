# LinkCrawler
Simple console application that will crawl the given webpage for image-tags and hyperlinks. If some of them is not working, an message will be sent to slack. Also information on all urls will be written to console output.

## App.Settings

| Key     				      | Usage           					   |
| --------------------------  |:--------------------------------------:|
| ```CheckImages```      			  | If true, <img src=".." will be checked |
| ```BaseUrl   ```   				  | Base url for site to crawl  	       |
| ```Slack.WebHook.Url```  | Url to the slack webhook     		   |
| ```Slack.WebHook.Bot.Name``` 	  | Custom name for slack bot   		   |
| ```Slack.WebHook.Bot.IconEmoji``` | Custom Emoji for slack bot  	       |
 
     
