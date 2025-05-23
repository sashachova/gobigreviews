FROM mcr.microsoft.com/dotnet/sdk:8.0
 
WORKDIR /app
 
# Install necessary dependencies for Chrome

RUN apt-get update && apt-get install -y \

    wget \

    unzip \

    curl \

    gnupg \

    ca-certificates \

    fonts-liberation \

    libasound2 \

    libatk-bridge2.0-0 \

    libdrm2 \

    libxkbcommon0 \

    libxss1 \

    libu2f-udev \

    libvulkan1 \

    xdg-utils \

    libgbm1 \

    libnss3 \

    libxrandr2 \

    libasound2 \

    libpangocairo-1.0-0 \

    libatk1.0-0 \

    libcairo-gobject2 \

    libgtk-3-0 \

    libgdk-pixbuf2.0-0 \
&& rm -rf /var/lib/apt/lists/*
 
# Get the latest stable Chrome version and install matching ChromeDriver

RUN CHROME_VERSION=$(curl -s "https://googlechromelabs.github.io/chrome-for-testing/LATEST_RELEASE_STABLE") && \

    echo "Installing Chrome version: $CHROME_VERSION" && \

    wget -O chrome-linux64.zip "https://storage.googleapis.com/chrome-for-testing-public/$CHROME_VERSION/linux64/chrome-linux64.zip" && \

    wget -O chromedriver-linux64.zip "https://storage.googleapis.com/chrome-for-testing-public/$CHROME_VERSION/linux64/chromedriver-linux64.zip" && \

    unzip chrome-linux64.zip && \

    unzip chromedriver-linux64.zip && \

    mv chrome-linux64 /opt/chrome && \

    mv chromedriver-linux64/chromedriver /usr/bin/chromedriver && \

    ln -s /opt/chrome/chrome /usr/bin/google-chrome && \

    chmod +x /usr/bin/chromedriver && \

    rm chrome-linux64.zip chromedriver-linux64.zip
 
# Verify Chrome and ChromeDriver versions

RUN google-chrome --version && chromedriver --version
 
# Install Java (required for Allure)

RUN apt-get update && apt-get install -y openjdk-11-jre && rm -rf /var/lib/apt/lists/*
 
# Install Allure CLI

RUN wget https://github.com/allure-framework/allure2/releases/download/2.24.0/allure-2.24.0.tgz && \

    tar -zxvf allure-2.24.0.tgz && \

    mv allure-2.24.0 /opt/allure && \

    ln -s /opt/allure/bin/allure /usr/bin/allure && \

    rm allure-2.24.0.tgz
 
# Copy project files

COPY . .
 
# Restore dependencies

RUN dotnet restore
 
# Build project

RUN dotnet build
 
# Create directories for test results

RUN mkdir -p TestResults allure-results allure-report
 
# Run tests with proper Chrome options

CMD ["sh", "-c", "export DISPLAY=:99 && \

    dotnet test --logger:\"trx;LogFileName=test-results.trx\" --results-directory:./TestResults || true && \

    if [ -f TestResults/test-results.trx ]; then \

        cp TestResults/*.trx allure-results/ 2>/dev/null || echo 'No TRX files to copy'; \

    fi && \

    if [ -d allure-results ] && [ \"$(ls -A allure-results)\" ]; then \

        allure generate allure-results --clean -o allure-report || echo 'Allure generation failed'; \

    else \

        echo 'No allure results found, creating empty report directory'; \

        mkdir -p allure-report; \

    fi"]
 