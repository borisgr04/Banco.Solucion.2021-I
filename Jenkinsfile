pipeline {
  agent any
  stages {
    stage('Print') {
      steps {
        echo 'Descargado'
      }
    }

    stage('Restore') {
      steps {
        bat 'dotnet restore'
      }
    }

  }
}