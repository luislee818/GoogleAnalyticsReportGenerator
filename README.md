#### Short story: 
A .NET console application renders an HTML report showing two sites' visitors of previous week by date and region, based on Google Analytics. 

After spending a weekend writing it, the effort for compiling the report was *cut from 15 minutes to 0 minute every week*. 

#### Long story:
The developer maintaining the site used to manually compile report every week - opens the Analytics site (not Google) in browser, grabs last week’s data into Excel, tweaks the wording and formats, copies into email and sends out.   

When that developer left and I was assigned, I did research on Google Analytics, tested and verified the results from both analytics services, wrote this application to automate the report generation.   

The application separates data retrieval (report building) and report generation, if we would change analytics service later, only the data retrieval part will be changed; uses interface abstracting out report generation from specific report formats. So *concerns are separated*.     

#### Usage & Sample
Specify settings in app.config before running the program:

- GAUsername
- GAPassword
- Site1TableId
- Site2TableId
- StartDate
- EndDate
- StyleSheetPath
- OutputFolderPath

A sample report (Sample.html) can be found under the root directory.