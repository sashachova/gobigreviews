FROM mcr.microsoft.com/dotnet/sdk:8.0
 
WORKDIR /app


RUN apt-get update && apt-get install -y wget unzip curl gnupg ca-certificates \
&& wget https://storage.googleapis.com/chrome-for-testing-public/124.0.6367.208/linux64/chrome-linux64.zip \
&& unzip chrome-linux64.zip \
&& mv chrome-linux64 /opt/chrome \
&& ln -s /opt/chrome/chrome /usr/bin/google-chrome
 
RUN wget https://storage.googleapis.com/chrome-for-testing-public/124.0.6367.208/linux64/chromedriver-linux64.zip \
&& unzip chromedriver-linux64.zip \
&& mv chromedriver-linux64/chromedriver /usr/bin/chromedriver \
&& chmod +x /usr/bin/chromedriver
 


RUN google-chrome --version && chromedriver --version
 


COPY . .
 


RUN wget https://github.com/allure-framework/allure2/releases/download/2.24.0/allure-2.24.0.tgz \
&& tar -zxvf allure-2.24.0.tgz \
&& mv allure-2.24.0 /opt/allure \
&& ln -s /opt/allure/bin/allure /usr/bin/allure
 
CMD dotnet restore && \

    dotnet build && \

    dotnet test --logger:"trx;LogFileName=test-results.trx" --results-directory:./TestResults && \

    mkdir -p allure-results && \

    cp TestResults/*.trx allure-results/ && \

    allure generate allure-results --clean -o allure-report

 