 /**
     * MY NOTES
     * Claim production ready. Meaning it should be able to run on standby. 
     * Movie - Hackers
     * Make unit tests and run GetTollFee with them
     * Make it runnable by either add a sln or csproj file
     * Import Classes
     * Need to add the missing vehicles 
     * 
     * Very ugly to rearrange the input of methodheads and have the same name of the method when referring to different method. Very confusing and mistakes can easily be made in the future.
     *  GetTollFee Else if like this is ugly confusing and risky, change it. - Changed to a switch case and included missing halfhours between 8:30 and to 14
     *
     *long diffInMillies = date.Millisecond - intervalStart.Millisecond;
      long minutes = diffInMillies/1000/60; 
     *Changed the above since since date.Millisecond gets the millisecond attribute of the date not the date represented in milliseconds
     */

#Changes

Created a project with a csproj file and copied the files to the project. The project is now runnabkle
     