FROM mcr.microsoft.com/dotnet/sdk:8.0
 


RUN apt-get update && apt-get install -y wget gnupg unzip curl && \

    wget -q -O - https://dl.google.com/linux/linux_signing_key.pub | apt-key add - && \

    echo "deb [arch=amd64] http://dl.google.com/linux/chrome/deb/ stable main" > /etc/apt/sources.list.d/google-chrome.list && \

    apt-get update && apt-get install -y google-chrome-stable
 


RUN CHROME_VERSION=$(google-chrome --version | grep -oP '\d+\.\d+\.\d+\.\d+') && \

    CHROMEDRIVER_VERSION=$(curl -sS https://chromedriver.storage.googleapis.com/LATEST_RELEASE_${CHROME_VERSION%.*}) && \

    wget -O /tmp/chromedriver.zip https://chromedriver.storage.googleapis.com/${CHROMEDRIVER_VERSION}/chromedriver_linux64.zip && \

    unzip /tmp/chromedriver.zip -d /usr/local/bin && \

    chmod +x /usr/local/bin/chromedriver
 
WORKDIR /app

 