# LinkCrawler
Simple C# console application that will crawl the given webpage for image-tags and hyperlinks. If some of them is not working (HttpStatusCode >= 300), an message will be sent to slack. Also response status will be written to console output.

## App.Settings

| Key     				      | Usage           					   |
| --------------------------  |:--------------------------------------:|
| ```CheckImages```      			  | If true, <img src=".." will be checked |
| ```BaseUrl   ```   				  | Base url for site to crawl  	       |
| ```Slack.WebHook.Url```  | Url to the slack webhook     		   |
| ```Slack.WebHook.Bot.Name``` 	  | Custom name for slack bot   		   |
| ```Slack.WebHook.Bot.IconEmoji``` | Custom Emoji for slack bot  	       |
| ```Slack.WebHook.Bot.MessageFormat``` | String format message that will be sent to slack  	       |
 
Example run on www.github.com:
![Example run on www.github.com](http://henrikm.com/content/images/2016/Feb/linkcrawler.png "Example run on www.github.com")

If it should stumble upon an link not working, the defined slack webhook will be notified
![Slack](http://henrikm.com/content/images/2016/Feb/homerbot2.jpg "Slack")

Read more about Slack incoming webhooks: https://api.slack.com/incoming-webhooks