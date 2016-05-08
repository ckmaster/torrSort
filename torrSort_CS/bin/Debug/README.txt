SETUP: 

All files below need to be in the same directory from which you launch the app.

sourceDir.txt -- this file needs to contain only the source directory, no spaces anywhere

torrSort_XML.xml -- this file should contain your rules in the following format 
(note: you need the asterisk after your searchPattern AND the backslash after your destFolder):

	<Rules>
		<searchPattern>The.Show.S01E01</searchPattern>
		<destFolder>C:\TheDestination\The Show Season 1\</destFolder>
	</Rules>
	
--

USE:

"Update Source Files" - Updates your source files list
"Show Source Files" - Shows your available files in your source directory (.avi, .mkv, .mp4)
"Run Rule List" - Runs the XML rules
"Show Rule XML" - Shows the XML rule list

--