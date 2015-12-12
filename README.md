# Command Line Deployment Tool
* Straightforward command line deployment tool for specific application types.
* These types include COM+, IIS, Windows Service, Exe, and FireDaemon applications.
* First it backups the application folder, then stops the service, copies new files to application folder, then restarts the application.
* If it gets error, then rollback process begins
* Other than that, after a successful deployment, there can be cases to get back to the previous version.
* This tool creates a rollback script after a successful deployment, this script can be used to get back to the previous version.
