
<!-- PROJECT LOGO -->
<br />
<p align="center">
  <a href="https://github.com/5c0rp264/C-Project">
    <img src="https://github.com/5c0rp264/C-Project/blob/main/CODE%20Livrable%201/logo.png" alt="Logo" width="300" height="206">
  </a>

  <p align="center">
    A backup software made for you by our team from ProSoft
    <br />
    <a href="https://github.com/5c0rp264/C-Project"><strong>Explore the docs »</strong></a>
    <br />
    <br />
    <a href="https://github.com/5c0rp264/C-Project/tree/main/Diagrammes">UML Diagrams</a>
    ·
    <a href="https://github.com/5c0rp264/C-Project/tree/main/CODE%20Livrable%201">Console Program</a>
    ·
    <a href="https://github.com/5c0rp264/C-Project/issues">Request Feature</a>
  </p>
</p>



<!-- TABLE OF CONTENTS -->
## Table of Contents

* [About the Project](#about-the-project)
  * [Software restrictions](#software-restrictions)
   
* [Version 1.0](#Version-1.0)
* [Getting Started](#getting-started)
  * [Prerequisites](#prerequisites)
  * [Installation](#installation)
* [Usage](#usage)
* [Project Management](#project-management)
* [Contributing](#contributing)
* [License](#license)
* [Contact](#contact)
* [Acknowledgements](#acknowledgements)



<!-- ABOUT THE PROJECT -->
## About The Project

<!-- [![Product Name Screen Shot][product-screenshot]](https://example.com) -->

Our team has just integrated the software publisher ProSoft.   
Under the responsibility of the CIO, we are in charge of managing the "EasySave" project which consists in developing a backup software.  
As any software of the ProSoft Suite, the software will be integrated into the pricing policy.  
- Unit price : 200 €HT 
- Annual maintenance contract 5/7 8-17h (updates included): 12% purchase price (Annual contract tacitly renewed with revaluation based on the SYNTEC index) 

During this project, we will have to ensure the development, the management of major and minor versions, but also the documentation (user and customer support).  
To ensure that our work can be taken over by other teams, we must work within certain constraints such as the tools used (see [Software restrictions](#(#software-restrictions))).  

Release dates and versions :
* Version 1.0 : 25/11/2020
* Version 2.0 : 07/12/2020
* Version 3.0 : 17/12/2020

### Software restrictions
This section lists each of the major technologies used to complete this project. For anymore details you can see the [acknowledgements](#acknowledgements) section.
* [Visual Studio 2019](https://visualstudio.microsoft.com/fr/)
* [GitHub](https://github.com/)
* [Framework .NET](https://www.microsoft.com/fr-fr/download/details.aspx?id=55167)
* [Visual Paradigm](https://www.visual-paradigm.com/)

## Version 1.0

The specifications of the first version of the software are as follows :  

The software is a Console application using .Net Core. 
It must allow the creation of up to 5 backup jobs. 

A backup job is defined by :  
 * An appellation  
 * A source directory   
 * A target directory   
 * One type  
  * Full  
  * Differential  
 * English  

The user may request the execution of one of the backup jobs or the sequential execution of the jobs.  
The directories (sources and targets) can be on local, external or network drives.  
All the elements of the source directory are concerned by the backup.   

Daily Log File : 

The software must write in real time in a daily log file the history of the actions of the backup jobs. The minimum expected information is : 

 * Timestamp   
 * Naming the backup job 
 * Full address of the Source file (UNC format) 
 * Full address of the destination file (UNC format) 
 * File Size  
 * File transfer time in ms (negative if error)   
  

Status File 
The software must record in real time, in a single file, the progress of the backup jobs.  The minimum expected information is :   

 * Timestamp   
 * Naming the backup job 
 * Backup job status (e.g. Active, Not Active...) 
If the job is active 
     * The total number of eligible files 
     * The size of the files to be transferred  
     * The progression          
     * Number of remaining files   
     * Size of remaining files   
     * Full address of the Source file being backed up 
     * Complete address of the destination file 
 

The locations of the two files described above (daily log and status) will have to be studied to work on the clients' servers. As a result, "c:\temp\" type locations are to be avoided. 

The files (daily log and status) and any configuration files will be in XML or JSON format.  In order to allow fast reading via Notepad, it is necessary to put line feeds between the XML (or JSON) elements. A pagination would be a plus. 

## Version 2.0
EasySave 1.0 has been distributed to many customers.  

Following a customer survey, the management decided to create a version 2.0 with the following improvements:  

* Graphical Interface 

Leaving the console mode. The application must now be developed in WPF under .Net Core. 

 * Unlimited number of jobs 

The number of backup jobs is now unlimited.  

 * Encryption via CryptoSoft software 

The software will have to be able to encrypt the files using CryptoSoft software (made during prosit 5).  Only the files with extensions defined by the user in the general settings should be encrypted. 

 * Evolution of the Daily Log file 

The daily log file must contain additional information: Time needed to encrypt the file (in ms)   

  - 0: no encryption  
  - \>0 : encryption time (in ms)  
  - <0 : error code  
 

* Business software 

If the presence of business software is detected, the software must prohibit the launch of a backup job. In the case of sequential jobs, the software must complete the current job and stop before launching the next job. The user will be able to define the business software in the general settings of the software. (Note: the calculator application can substitute the business software during demonstrations).  

 Note: Some clients want to have, for each backup job, an interface allowing them to act on it via three functions (Play, Pause, Stop). The sales department asked that this function not be taken into account in version 2.0.  However, this function will be in the specifications for version 3.0. 


You will find below the version comparison table  


<!-- GETTING STARTED -->
## Getting Started

To get a local copy up and running follow these simple example steps. But before anything else you should be sure to have Visual Studio 2019 in order to run the program.


### Prerequisites

Once you will have set up your computer You will have to clone this project.
* Git
```sh
git clone https://github.com/5c0rp264/C-Project
```
Then refer to the usage section and you will be ready to go !
<!-- USAGE EXAMPLES -->
## Usage

Now that you have cloned this repository you can open the project in visual studio and run it as you wish. Because of the current version (1.0) you may need to delete the database json file. If you are using the first release version (branch v1.0) you can just use the .exe file. Then you can add / edit / delete any backup work via the main interface delivered by the view.

_For more examples, please refer to the [Documentation](https://example.com)_

<!-- Management -->
## Project Management

In order to allow each member of our team to create and participate in the most efficient way possible we had to put together a project management strategy. This strategy translates into our collaborative work via the GitHub platform, where our code and the use of design templates such as the VCM have allowed us to easily distribute tasks. Moreover, in a concern of perenniality of the application, these design patterns will allow us to pass in a second time to a graphic interface in a much simpler way.  

Beyond GitHub we also collaborated via Azure Devops via a board, adding To Do list so that everyone knows the remaining tasks and have a simpler and clearer vision of our Github repository. But also to set up pipelines to certify that our code is working properly and that we are using good practices during this project.

<!-- CONTRIBUTING -->
## Contributing

Contributions are what make the open source community such an amazing place to be learn, inspire, and create. Any contributions you make would be **greatly appreciated**.

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request



<!-- LICENSE -->
## License

Distributed under the MIT License. See `LICENSE.txt` for more information.



<!-- CONTACT -->
## Contact

Tanguy Lhinares - [Website](https://www.lhinares-technologies.com/) - tanguy.lhinares@viacesi.fr  
Quentin Aoustin - [Resume](http://quentinaoustin.redirectme.net/ISN/CV.html) - quentin.aoustin@viacesi.fr  
Vincent Jacques - [LinkedIn](https://www.linkedin.com/in/vincent-jacques-a173bb1a2/) - vincent.jacques@viacesi.fr  
Jérémy Gabriel - jeremy.gabriel@viacesi.fr  
Rémi Mounier - remi.mounier@viacesi.fr  

Project Link: [https://github.com/5c0rp264/C-Project/](https://github.com/5c0rp264/C-Project/)



<!-- ACKNOWLEDGEMENTS -->
## Acknowledgements
* [NOTHING HERE YET](https://example.com/)

