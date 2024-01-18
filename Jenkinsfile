pipeline {
    agent any
    stages {
        stage('test Branch Deploy Code') {
            when {
                branch 'test'
            }
            steps {
                sh """
                cd /var/www/dotnet-test/API
                sudo git stash
                sudo git pull origin test
                sudo /opt/dotnet/dotnet restore
                sudo /opt/dotnet/dotnet publish mongodb-dotnet-example.csproj --output /var/www/dotnet-test/API/build
                sudo /opt/dotnet/dotnet test /var/www/dotnet-test/Tests/MongoTests/
                sudo systemctl restart dotnet-test.service 
                #test#
                """
            }
        }
        stage('preprod Branch Deploy Code') {
            when {
                branch 'preprod'
            }
            steps {
                sh """
                cd /var/www/dotnet-preprod/API
                sudo git stash
                sudo git pull origin preprod
                sudo /opt/dotnet/dotnet restore
                sudo /opt/dotnet/dotnet publish mongodb-dotnet-example.csproj --output /var/www/dotnet-preprod/API/build
                sudo /opt/dotnet/dotnet test /var/www/dotnet-preprod/Tests/MongoTests/
                sudo systemctl restart dotnet-preprod.service
                """
           }
        }
        stage('main Branch Deploy Code') {
            when {
                branch 'main'
            }
            steps {
                sh """
                cd /var/www/dotnet-prod/API
                sudo git stash
                sudo git pull origin main
                sudo /opt/dotnet/dotnet restore
                sudo /opt/dotnet/dotnet publish mongodb-dotnet-example.csproj --output /var/www/dotnet-prod/API/build
                sudo /opt/dotnet/dotnet test /var/www/dotnet-prod/Tests/MongoTests/
                sudo systemctl restart dotnet-prod.service
                """
           }
        }
    }
}
