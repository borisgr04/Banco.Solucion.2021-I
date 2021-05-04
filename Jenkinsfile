pipeline {
  agent any
  stages {
    stage('Cleanup Workspace') {
      steps {
        cleanWs()
        echo 'Cleaned Up Workspace For Project'
      }
    }

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

    stage('Build Deploy Code') {
      when {
        branch 'develop'
      }
      steps {
        echo 'Rama Develop'
      }
    }

    stage('Build Deploy Code Master') {
      when {
        branch 'master'
      }
      steps {
        echo 'Rama Master'
      }
    }

  }
}