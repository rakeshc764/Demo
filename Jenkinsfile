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
                sudo systemctl reload dotnet-test.service
                 
                """
            }
        }
        stage('preprod Branch Deploy Code') {
            when {
                branch 'preprod'
            }
            steps {
                sh """
                echo "Building Artifact from Develop branch"
                """
                sh """
                echo "Deploying Code from Develop branch"
                """
           }
        }
        stage('main Branch Deploy Code') {
            when {
                branch 'main'
            }
            steps {
                sh """
                echo "Building Artifact from Preprod branch"
                """
                sh """
                echo "Deploying Code from Preprod branch"
                """
           }
        }
    }
}
