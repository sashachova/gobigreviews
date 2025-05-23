FROM mcr.microsoft.com/dotnet/sdk:8.0
 


RUN apt-get update && apt-get install -y wget gnupg unzip curl && \

    wget -q -O - https://dl.google.com/linux/linux_signing_key.pub | apt-key add - && \

    echo "deb [arch=amd64] http://dl.google.com/linux/chrome/deb/ stable main" > /etc/apt/sources.list.d/google-chrome.list && \

    apt-get update && apt-get install -y google-chrome-stable && \

    rm -rf /var/lib/apt/lists/*
 


RUN CHROME_VERSION=$(google-chrome --version | grep -oP '\d+\.\d+\.\d+\.\d+') && \

    CHROMEDRIVER_VERSION=$(curl -sSL https://chromedriver.storage.googleapis.com/LATEST_RELEASE_${CHROME_VERSION%.*}) && \

    wget -O /tmp/chromedriver.zip https://chromedriver.storage.googleapis.com/${CHROMEDRIVER_VERSION}/chromedriver_linux64.zip && \

    unzip /tmp/chromedriver.zip -d /usr/local/bin && chmod +x /usr/local/bin/chromedriver
 


RUN wget https://github.com/allure-framework/allure2/releases/download/2.34.0/allure-2.34.0.tgz && \

    tar -zxvf allure-2.34.0.tgz && \

    mv allure-2.34.0 /opt/allure && \

    ln -s /opt/allure/bin/allure /usr/bin/allure
 
WORKDIR /app

 