pipeline {
  agent any
  stages {
    stage('Checkout') {
      steps {
        checkout scm
      }
    }

    stage('Restore') {
      steps {
        bat 'dotnet restore'
        bat 'dotnet clean'
      }
    }

    stage('Build') {
      steps {
        bat 'dotnet build  --configuration Release'
      }
    }

    stage('Test') {
      steps {
        bat 'dotnet test --logger trx;LogFileName=unit_tests.trx'
      }
    }

    stage('Print ') {
      steps {
        echo 'Rama'
      }
    }

  }
}