# Базовий образ із .NET SDK та Chrome

FROM mcr.microsoft.com/dotnet/sdk:8.0
 
# Встановлюємо Chrome

RUN apt-get update && apt-get install -y \

    wget gnupg unzip curl \
&& wget -q -O - https://dl.google.com/linux/linux_signing_key.pub | gpg --dearmor > /usr/share/keyrings/google.gpg \
&& echo "deb [arch=amd64 signed-by=/usr/share/keyrings/google.gpg] http://dl.google.com/linux/chrome/deb/ stable main" > /etc/apt/sources.list.d/google.list \
&& apt-get update \
&& apt-get install -y google-chrome-stable
 
# Встановлюємо ChromeDriver (з тією ж версією, що Chrome)

RUN CHROME_VERSION=$(google-chrome-stable --version | grep -oP "\d+\.\d+\.\d+") \
&& CHROMEDRIVER_VERSION=$(curl -s "https://googlechromelabs.github.io/chrome-for-testing/last-known-good-versions-with-downloads.json" | grep -A 5 "\"$CHROME_VERSION\"" | grep "linux64" | grep -oP 'https://[^"]+') \
&& curl -Lo chromedriver.zip "$CHROMEDRIVER_VERSION" \
&& unzip chromedriver.zip \
&& mv chromedriver /usr/bin/chromedriver \
&& chmod +x /usr/bin/chromedriver \
&& rm chromedriver.zip
 
# Копіюємо все в контейнер

WORKDIR /app

COPY . .
 
# Встановлюємо Allure CLI

RUN wget https://github.com/allure-framework/allure2/releases/download/2.24.0/allure-2.24.0.tgz \
&& tar -zxvf allure-2.24.0.tgz \
&& mv allure-2.24.0 /opt/allure \
&& ln -s /opt/allure/bin/allure /usr/bin/allure
 
# Відкритий порт (опціонально)

EXPOSE 5000
 
# Команди за замовчуванням (можеш змінити під себе)

CMD dotnet restore && \

    dotnet build && \

    dotnet test --logger:"trx;LogFileName=test-results.trx" --results-directory:./TestResults && \

    mkdir -p allure-results && \

    cp TestResults/*.trx allure-results/ && \

    allure generate allure-results --clean -o allure-report

 