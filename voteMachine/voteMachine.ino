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
#include "web_response.h"

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

const byte int_pin_green = D6;
const byte int_pin_red = D5;

unsigned long timer_green;
unsigned long timer_red;

WiFiServer server(80);
WiFiClient client;

SSD1306 display(0x3c, SDA, SCL);

String line[MAX_LINES];
String ip_string;
String vote_option_red;
String vote_option_green;
String serial_data, serial_data_value;
const String default_red_option = "Red";
const String default_green_option = "Green";
bool serial_data_complete;

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
        line[i].reserve(LINE_LENGTH);
        line[i] = "";
    }

    ip_string.reserve(20);
    vote_option_red.reserve(12);
    vote_option_green.reserve(12);
    serial_data.reserve(20);
    serial_data_value.reserve(20);
    serial_data_complete = false;

    serial_data = "";
    serial_data_value = "";
    ip_string = "";
    vote_option_red = default_red_option;
    vote_option_green = default_green_option;

    IPAddress ip = WiFi.localIP();
    for (int i = 0; i < 4; i++)
    {
        ip_string += i  ? "." + String(ip[i]) : String(ip[i]);
    }

    line[0] = "Set question with";
    line[1] = "VoteMachineApp";
    line[3] = "Ip: " + ip_string;

    updateScreen();

    update_display = false;
    int_green_triggered = false;
    int_red_triggered = false;
}

void loop()
{
    serialEvent();

    if(serial_data_complete)
    {
        handle_command();
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

/* Read serial Data */
void serialEvent()
{
    while (Serial.available())
    {
        char read_byte = (char)Serial.read();

        if (read_byte == '\n')
        {
            serial_data_complete = true;
            return;
        }

        serial_data += read_byte;
    }
}

/* Write text to display */
void updateScreen()
{
    display.clear();

    line[MAX_LINES-1] = vote_option_green + ": " +
        String(vote_count_green) + " | " + vote_option_red + ": " +
        String(vote_count_red);

    for(int i = 0; i < MAX_LINES; i++)
    {
        display.drawString(0, LINE_HEIGHT * i + (i > 2 ? 10 : 0),  line[i]);
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

/* Check if web client is available */
bool check_connection()
{
    client = server.available();

    if (!client)
    {
        return false;
    }

    return true;
}

/* Process incoming serial command */
void handle_command(void)
{
    /* First byte determines command,
    rest is data to set */
    char command = serial_data[0];

    switch(command)
    {
        case '1':  // Write to display line 1
            line[0] = serial_data.substring(1);
            update_display = true;
            break;

        case '2': // Write to display line 2
            line[1] = serial_data.substring(1);
            update_display = true;
            break;

        case '3': // Write to display line 3
            line[2] = serial_data.substring(1);
            update_display = true;
            break;

        case '4': // Write to display line 4
            line[3] = serial_data.substring(1);
            update_display = true;
            break;

        case 'R': // Red button vote option
            vote_option_red = serial_data.substring(1);
            update_display = true;
            break;

        case 'G': // Red button vote option
            vote_option_green = serial_data.substring(1);
            update_display = true;
            break;

        case 'C': // Clear/Reset
            for (int i = 0; i < MAX_LINES; i++)
            {
                line[i] = "";
            }

            vote_count_red = 0;
            vote_count_green = 0;

            vote_option_red = default_red_option;
            vote_option_green = default_green_option;

            update_display = true;

            break;

        case 'S': // Status
            Serial.println("Connected to voteMachine!");
            Serial.println(ip_string);

            for (int i = 0; i < MAX_LINES; i++)
            {
                if(line[i] != "")
                {
                    Serial.println(line[i]);
                }
            }

            break;

        default:
            break;
    }

    serial_data = "";
    serial_data_value = "";
    serial_data_complete = false;
}

/* Show webpage for client */
void clientResponse()
{
    client.println("HTTP/1.1 200 OK");
    client.println("Content-Type: text/html");
    client.println("");

    String web_response = "";

    for(int i = 0; i < 4; i++)
    {
        web_response += i != 3 ? line[i] + "<br>" : line[i];
    }

    web_response += "<li>" + vote_option_green + "<span class=\"ui-li-count\">"
        + String(vote_count_green) + "<\/span><\/li>";
    web_response += "<li>" + vote_option_red + "<span class=\"ui-li-count\">"
        + String(vote_count_red) + "<\/span><\/li>";

    client.println(web_response_string_head + web_response
        + web_response_string_tail);
}
