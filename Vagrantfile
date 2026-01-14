# -*- mode: ruby -*-
# vi: set ft=ruby :

Vagrant.configure("2") do |config|
  # Використовуємо образ Ubuntu 22.04 LTS
  config.vm.box = "bento/ubuntu-22.04"

  # Налаштування провайдера (VirtualBox)
  config.vm.provider "virtualbox" do |vb|
    vb.memory = "2048" # Виділяємо 2 ГБ оперативної пам'яті
    vb.cpus = 2
    vb.gui = false
  end

  # PROVISIONING: Скрипт, який виконається при першому запуску [cite: 1436]
  config.vm.provision "shell", inline: <<-SHELL
    # 1. Оновлення списку пакетів
    echo "Update packages..."
    sudo apt-get update

    # 2. Встановлення необхідних утиліт (git, unzip)
    echo "Installing Git..."
    sudo apt-get install -y git unzip

    # 3. Встановлення .NET SDK 8.0 (Microsoft instructions)
    # Зверніть увагу: інструкції для Ubuntu 22.04 відрізняються від 18.04 у лекції
    echo "Installing .NET SDK 8.0..."
    sudo apt-get install -y dotnet-sdk-8.0

    # 4. Перевірка встановлення
    dotnet --info

    # 5. Клонування репозиторію з Лабораторної 1 (Метод з "Tips" )
    # ЗАМІНІТЬ ПОСИЛАННЯ НИЖЧЕ НА ВАШ РЕПОЗИТОРІЙ!
    echo "Cloning repository..."
    if [ ! -d "/home/vagrant/AcademicTrackerLab" ]; then
      git clone https://github.com/sashkotenk/Cross.git /home/vagrant/AcademicTrackerLab
    else
      echo "Repository already exists."
    fi

    # 6. Збірка та запуск тестів (щоб переконатися, що все працює)
    echo "Running Tests..."
    cd /home/vagrant/AcademicTrackerLab
    dotnet test

    echo "Deployment Complete! To run the app, ssh into the machine."
  SHELL
end