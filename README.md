# Tfs auto task creator :

While working in Agile, we normally have sprint of 2 or 3 weeks. 
So at the start of each sprint we need to create tasks for each PBI. Some standard task are common across all PBI. So I thought of automating that using TFS C# Api. So this utility will help you to create these automatically.

#How it works?
This will work in 2 modes. One is through command line or through windows form application. If you provide command line input which should be PBI Id in tfs, then it will validate PBI and create task under it. Another way is windows form application, if you don’t provide any command link argument then form application will be launched and you will get basic UI for entering PBI Id. 
