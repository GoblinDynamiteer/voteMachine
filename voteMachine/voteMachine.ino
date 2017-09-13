/*
    voteMachine!
    by Johan Kampe & Dylan Saleh

    Hookup:

    SSD1306     SDA D2
    SSD1306     SCL D1
    HC-06       RX  TX
    HC-06       TX  RX
    BUTTON_G    D6
    BUTTON_R    D5


 */

#include <ESP8266WiFi.h>
#include <WiFiSettings.h> // SSID & Password
#include <Wire.h>
#include "SSD1306.h"

#define PIN_WIRE_SDA 4
#define PIN_WIRE_SCL 5
#define SDA PIN_WIRE_SDA
#define SCL PIN_WIRE_SCL

#define LED_BUILTIN 16
#define BUILTIN_LED 16

#define D0 1
#define D1 5
#define D2 4
#define D3 0
#define D4 2
#define D5 14
#define D6 12
#define D7 13
#define D8 15
#define D9 3
#define D10 1

#define SERIAL_BUFFER_SIZE 30
#define LINE_HEIGHT 10
#define LINE_LENGHT 30
#define MAX_LINES 5
#define BUTTON_DELAY 200
const char COMMAND_END = '\n';

const byte int_pin_green = D6;
const byte int_pin_red = D5;

unsigned long timer_green;
unsigned long timer_red;

WiFiServer server(80);
WiFiClient client;
char * ip_string;

SSD1306 display(0x3c, SDA, SCL);

char * line[MAX_LINES];
int vote_green, vote_red;
bool update;

void setup()
{
    display.init();
    display.flipScreenVertically();
    display.setTextAlignment(TEXT_ALIGN_LEFT);
    display.setFont(ArialMT_Plain_10);

    Serial.begin(9600);
    delay(10);

    /* Connect to WiFi */
    WiFi.begin(WIFI_SSID, WIFI_PASSWORD);

    while (WiFi.status() != WL_CONNECTED)
    {
        delay(500);
    }

    /* Start server */
    server.begin();

    /* Interrupt pins */
    pinMode(int_pin_green, INPUT_PULLUP);
    pinMode(int_pin_red, INPUT_PULLUP);

    attachInterrupt(int_pin_green, int_func_green, FALLING);
    attachInterrupt(int_pin_red, int_func_red, FALLING);

    timer_red = millis();
    timer_green = millis();

    for (int i = 0; i < MAX_LINES; i++)
    {
        line[i] = (char *)malloc(LINE_LENGHT);
    }

    IPAddress ip = WiFi.localIP();

    ip_string = (char *)malloc(10);
    sprintf(ip_string, "%d.%d.%d.%d", ip[0], ip[1], ip[2], ip[3]);

    strcpy(line[0], "Set text");
    strcpy(line[1], "with APP!");
    strcpy(line[2], ip_string);
    strcpy(line[3], "\0");

    updateScreen();

    update = false;
}

void loop()
{
    /* Get command from bluetooth serial */
    char * data;
    data = readSerial();

    /* Switch on first char in sent command */
    switch (data[0])
    {
        case '1':  // Line 1
            strcpy(line[0], data + 1);
            update = true;
            Serial.println("Line 1 set");
            break;

        case '2': // Line 2
            strcpy(line[1], data + 1);
            update = true;
            Serial.println("Line 2 set");
            break;

        case '3': // Line 3
            strcpy(line[2], data + 1);
            update = true;
            Serial.println("Line 3 set");
            break;

        case '4': // Line 4
            strcpy(line[3], data + 1);
            update = true;
            Serial.println("Line 4 set");
            break;

        case 'C': // Clear/Reset
            for (int i = 0; i < MAX_LINES; i++)
            {
                strcpy(line[i], "\0");
            }

            vote_red = 0;
            vote_green = 0;

            update = true;

            break;

        case 'S': // Statusl
            /* TODO: Add current display data */
            Serial.println("Connected to voteMachine!");
            Serial.println("Web IP: " + String(ip_string));
            Serial.println("GREEN: " + String(vote_green));
            Serial.println("RED: " + String(vote_red));
            break;

        default:
            break;
    }

    if(update)
    {
        updateScreen();
        update = false;
    }

    if(check_connection())
    {
        clientResponse();
    }
}

/* Read serial command (from bluetooth) */
char * readSerial()
{
    char command[SERIAL_BUFFER_SIZE + 1];

    int command_size = Serial.readBytesUntil(
        COMMAND_END,
        command,
        SERIAL_BUFFER_SIZE
    );

    if(command_size > 0)
    {
        command[command_size] = '\0';
        return command;
    }

    return "0";

}

/* Write text to display */
void updateScreen()
{
    sprintf(line[MAX_LINES-1], "Green: %i | Red: %i", vote_green, vote_red);

    display.clear();

    for(int i = 0; i < MAX_LINES; i++)
    {
        display.drawString(0, LINE_HEIGHT * i,  String(line[i]));
    }

    display.display();
}

/* Green button interrupt function */
void int_func_green()
{
    if(millis() - timer_green > BUTTON_DELAY)
    {
        vote_green++;
        Serial.println("Green votes: " + String(vote_green));
        update = true;
        timer_green = millis();
    }
}

/* Red button interrupt function */
void int_func_red()
{
    if(millis() - timer_red > BUTTON_DELAY)
    {
        vote_red++;
        Serial.println("Red votes: " + String(vote_red));
        update = true;
        timer_red = millis();
    }
}

bool check_connection()
{
    /* Check for new client connected to server */
    client = server.available();

    if (!client)
    {
        return false;
    }

    while(!client.available())
    {
        delay(1);
    }

    /* Read request */
    String request = client.readStringUntil('\r');
    Serial.println(request);
    client.flush();

    return true;
}

/* Show webpage for client */
void clientResponse()
{
    client.println("HTTP/1.1 200 OK");
    client.println("Content-Type: text/html");
    client.println("");
    client.println("<!DOCTYPE HTML>");

    client.println("<html>");
    client.println("<title>voteMachineWeb!</title>");
    client.println("<img src=\"https://ibin.co/3aERSRVvPjON.jpg\"");
    client.println("<p>Green votes: " + String(vote_green));
    client.println("<p>Red votes: " + String(vote_red));
    client.println("</html>");
}
