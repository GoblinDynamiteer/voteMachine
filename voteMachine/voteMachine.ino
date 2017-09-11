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


#include <ESP8266WiFi.h>
#include <WiFiSettings.h> // SSID & Password
#include <Wire.h>
#include "SSD1306.h"

#define SERIAL_BUFFER_SIZE 30
#define LINE_HEIGHT 15
#define BUTTON_DELAY 100
const char COMMAND_END = '\n';

const byte int_pin_green = D6;
const byte int_pin_red = D5;

unsigned long timer_green;
unsigned long timer_red;

//WiFiServer server(80);
//WiFiClient client;

SSD1306 display(0x3c, SDA, SCL);

char * line[4];
int vote_green, vote_red;

void setup()
{
    display.init();
    display.flipScreenVertically();
    display.setTextAlignment(TEXT_ALIGN_LEFT);
    display.setFont(ArialMT_Plain_10);

    Serial.begin(9600);
    //Serial.setTimeout(1000);
    delay(10);

    /* Interrupt pins */
    pinMode(int_pin_green, INPUT_PULLUP);
    pinMode(int_pin_red, INPUT_PULLUP);

    /* Setup interrupts */ /*
    attachInterrupt(digitalPinToInterrupt(
        int_pin_green), int_func_green, FALLING);
    attachInterrupt(digitalPinToInterrupt(
        int_pin_red), int_func_red, FALLING); */

    attachInterrupt(int_pin_green, int_func_green, LOW);
    attachInterrupt(int_pin_red, int_func_red, FALLING);

    timer_red = millis();
    timer_green = millis();

    for (int i = 0; i < 4; i++)
    {
        line[i] = (char *)malloc(10);
    }

    strcpy(line[0], "Set text");
    strcpy(line[1], "with APP!");
    strcpy(line[2], "\0");
    strcpy(line[3], "\0");
    setText();
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
            setText();
            Serial.println("Line 1 set");
            break;

        case '2': // Line 2
            strcpy(line[1], data + 1);
            setText();
            Serial.println("Line 2 set");
            break;

        case '3': // Line 3
            strcpy(line[2], data + 1);
            setText();
            Serial.println("Line 3 set");
            break;

        case '4': // Line 4
            strcpy(line[3], data + 1);
            setText();
            Serial.println("Line 4 set");
            break;

        case 'C': // Clear/Reset
            for (int i = 0; i < 4; i++)
            {
                strcpy(line[i], "\0");
            }

            vote_red = 0;
            vote_green = 0;

            setText();

            break;

        case 'S': // Status
            /* TODO: Add current display data */
            Serial.println("Connected to voteMachine!");
            Serial.println("GREEN: " + String(vote_green));
            Serial.println("RED: " + String(vote_red));
            break;

        default:
            break;
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
        /* Show command size on display */
        sprintf(line[3], "CMD SIZE: %i", command_size);
        setText();

        command[command_size] = '\0';

        return command;
    }

    return "0";

}

/* Write text to display */
void setText()
{
    display.clear();
    display.drawString(0, 0,  String(line[0]));
    display.drawString(0, 10, String(line[1]));
    display.drawString(0, 20, String(line[2]));
    display.drawString(0, 30, String(line[3]));
    display.display();
}

/* Green button interrupt function */
void int_func_green()
{
    if(millis() - timer_green > BUTTON_DELAY)
    {
        vote_green++;
        Serial.println("Green votes: " + String(vote_green));

        sprintf(line[3], "GR: %i RE: %i", vote_green, vote_red);
        setText();

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

        sprintf(line[3], "GR: %i RE: %i", vote_green, vote_red);
        setText();

        timer_red = millis();
    }
}
