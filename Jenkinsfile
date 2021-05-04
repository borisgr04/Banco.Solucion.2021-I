pipeline {
  agent any
  stages {
    stage('Checkout') {
      steps {
        checkout scm
      }
    }
     
    stage('Print') {
      steps {
        echo 'Descargado'
      }
    }

    stage('Restore') {
      steps {
        sh 'dotnet restore'
      }
    }

  }
}
