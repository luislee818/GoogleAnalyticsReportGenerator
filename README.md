#### Short story: 
A .NET console application renders an HTML report showing two sites' visitors of previous week by date and region, based on Google Analytics. 

After spending a weekend writing it, the effort for compiling the report was *cut from 15 minutes to 0 minute every week*. 

#### Long story:
The developer maintaining the site used to manually compile report every week - opens the Analytics site (not Google) in browser, grabs last week’s data into Excel, tweaks the wording and formats, copies into email and sends out.   

When that developer left and I was assigned, I did research on Google Analytics, tested it and verified the results from both analytics services were basically the same, then wrote this application utilizing Google Analytics API and automating the report generation.   

The application separates data retrieval (report building) and report generation, if in the future we would change analytics service, only the data retrieval part will be changed; also uses interface to abstract out report generation from its underlying format. So *concerns are separated*.     

#### Usage
Specify settings in app.config before running the program:

- GAUsername
- GAPassword
- Site1TableId
- Site2TableId
- StartDate
- EndDate
- StyleSheetPath
- OutputFolderPath