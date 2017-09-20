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
#define LINE_LENGTH 30
#define MAX_LINES 5
#define BUTTON_DELAY 200
const char COMMAND_END = '\n';

const byte int_pin_green = D6;
const byte int_pin_red = D5;

unsigned long timer_green;
unsigned long timer_red;

WiFiServer server(80);
WiFiClient client;

SSD1306 display(0x3c, SDA, SCL);

char * line[MAX_LINES];
char * ip_string;
char * vote_option_red;
char * vote_option_green;

int vote_count_green, vote_count_red;
bool update_display, int_red_triggered, int_green_triggered;

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
        line[i] = (char *)malloc(LINE_LENGTH);
    }

    ip_string = (char *)malloc(20);
    vote_option_red = (char *)malloc(12);
    vote_option_green = (char *)malloc(12);

    IPAddress ip = WiFi.localIP();

    sprintf(ip_string, "IP: %d.%d.%d.%d", ip[0], ip[1], ip[2], ip[3]);

    strcpy(line[0], "Set text");
    strcpy(line[1], "with APP!");
    strcpy(line[2], "\0");
    strcpy(line[3], ip_string);
    strcpy(vote_option_red, "Red");
    strcpy(vote_option_green, "Green");

    updateScreen();

    update_display = false;
    int_green_triggered = false;
    int_red_triggered = false;
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
            update_display = true;
            Serial.println("Line 1 set");
            break;

        case '2': // Line 2
            strcpy(line[1], data + 1);
            update_display = true;
            Serial.println("Line 2 set");
            break;

        case '3': // Line 3
            strcpy(line[2], data + 1);
            update_display = true;
            Serial.println("Line 3 set");
            break;

        case '4': // Line 4
            strcpy(line[3], data + 1);
            update_display = true;
            Serial.println("Line 4 set");
            break;

        case 'R': // Red button vote option
            strcpy(vote_option_red, data + 1);
            update_display = true;
            Serial.println("Red option set");
            break;

        case 'G': // Red button vote option
            strcpy(vote_option_green, data + 1);
            update_display = true;
            Serial.println("Green option set");
            break;

        case 'C': // Clear/Reset
            for (int i = 0; i < MAX_LINES; i++)
            {
                strcpy(line[i], "\0");
            }

            vote_count_red = 0;
            vote_count_green = 0;

            strcpy(vote_option_red, "Red");
            strcpy(vote_option_green, "Green");

            update_display = true;

            break;

        case 'S': // Status
            /* TODO: Add current display data */
            Serial.println("Connected to voteMachine!");
            Serial.println(String(ip_string));

            for (int i = 0; i < MAX_LINES; i++)
            {
                if(line[i][0] != '\0')
                {
                    Serial.println(String(line[i]));
                }
            }

            break;

        default:
            break;
    }

    if(int_green_triggered)
    {
        Serial.println("Green votes: " + String(vote_count_green));
        timer_green = millis();
        int_green_triggered = false;;
    }

    if(int_red_triggered)
    {
        Serial.println("Red votes: " + String(vote_count_red));
        timer_red = millis();
        int_red_triggered = false;
    }

    if(update_display)
    {
        updateScreen();
        update_display = false;
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
    sprintf(line[MAX_LINES-1], "%s: %i | %s: %i",
        vote_option_green, vote_count_green, vote_option_red, vote_count_red);

    display.clear();

    for(int i = 0; i < MAX_LINES; i++)
    {
        display.drawString(0, LINE_HEIGHT * i + (i > 2 ? 10 : 0),  String(line[i]));
    }

    display.drawHorizontalLine(0, LINE_HEIGHT * 4 - 2 , 128);

    display.display();
}

/* Green button interrupt function */
void int_func_green()
{
    if(!int_green_triggered && millis() - timer_green > BUTTON_DELAY)
    {
        vote_count_green++;
        int_green_triggered = true;
        update_display = true;
    }
}

/* Red button interrupt function */
void int_func_red()
{
    if(!int_red_triggered && millis() - timer_red > BUTTON_DELAY)
    {
        vote_count_red++;
        int_red_triggered = true;
        update_display = true;
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
    const String web_title = "voteMachineWeb!";
    const String font_name = "Oswald";
    const String font_url = "https://fonts.googleapis.com/css?family=" + font_name;
    const String font_color = "#fff";
    const String bg_color = "#000";
    const String font_size ="70";

    client.println("HTTP/1.1 200 OK");
    client.println("Content-Type: text/html");
    client.println("");
    client.println("<!DOCTYPE HTML>");

    client.println("<html>");
    client.println("<title>" + web_title + "</title>");
    client.println("<link href=\"" + font_url + "\" rel=\"stylesheet\">");
    client.println("<body bgcolor=" + bg_color + " style=\"font-family: '" +
        font_name + "';color:" + font_color + ";font-size: " + font_size + "px\">");

    client.println(String(line[0]) + "<br>");
    client.println(String(line[1]) + "<br>");
    client.println(String(line[2]) + "<p>");

    client.println("<font style=\"color:green;\">"
        + String(vote_option_green) + ": "
        + String(vote_count_green) + "</font> | ");
    client.println("<font style=\"color:red;\">"
        + String(vote_option_red) + ": "
        + String(vote_count_red) + "</font>");
    client.println("</html>");
}
