pipeline {
    agent any

    stages {
        stage('Build') {
            steps {
                script {
                    sh '''
                        cd /var/www/test
                        sudo git stash
                        sudo git pull origin test
                        sudo rm -rf /var/www/test/API/Build
                        sudo /opt/dotnet restore
                        sudo /opt/dotnet publish --framework net7.0 /var/www/test/API/mongodb-dotnet-example.csproj --configuration Release --output /var/www/test/API/Build
                        sudo pkill -f mongodb-dotnet-example
                        sudo rm -rf /var/www/test/API/Build/web.config
                        sudo rm -rf /var/www/test/API/Build/appsettings.PreProd.json
                        sudo rm -rf /var/www/test/API/Build/appsettings.Prod.json
                    '''
                }
            }
        }

        stage('Run Tests') {
            steps {
                script {
                    sh 'sudo /opt/dotnet test /var/www/test/Tests/MongoTests/'
                }
            }
        }

        stage('Deploy') {
            steps {
                script {
                    sh '''
                        sudo sshpass -p 'Welcome123' ssh testadmin@98.70.15.172 "C:\\Windows\\SysWOW64\\inetsrv\\appcmd.exe stop site /site.name:\"test.com\""
                        echo "apppool stop"
                        sudo sshpass -p 'Welcome123' ssh testadmin@98.70.15.172 "C:\\Windows\\SysWOW64\\inetsrv\\appcmd.exe stop apppool /apppool.name:\"test.com\""
                        echo "apppool stop"
                        sudo chmod 755 /var/www/test/API/Build -R
                        cd /var/www/test/API/Build
                        sudo lftp -e  "mirror --overwrite -R /var/www/test/API/Build .;quit" -u test,Welcome@123 98.70.15.172
                        sudo sshpass -p 'Welcome123' ssh testadmin@98.70.15.172 "C:\\Windows\\SysWOW64\\inetsrv\\appcmd.exe start site /site.name:\"test.com\""
                        echo "apppool started"
                        sudo sshpass -p 'Welcome123' ssh testadmin@98.70.15.172 "C:\\Windows\\SysWOW64\\inetsrv\\appcmd.exe start apppool /apppool.name:\"test.com\""
                        echo "site started"
                        #test
                    '''
                }
            }
        }
    }
}
