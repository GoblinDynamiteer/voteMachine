#include <SSD1306.h>

#define SERIAL_BUFFER_SIZE 30
#define LINE_HEIGHT 15
#define BUTTON_DELAY 100
const char COMMAND_END = '\n';

const byte int_pin_green = 2;
const byte int_pin_red = 3;

unsigned long timer_green;
unsigned long timer_red;

SSD1306 display(0x3c, D3, D5);

char * line[4];
int vote_green, vote_red;

/* Write text to display */
void setText(String text, int x = 0, int y = 0)
{
    display.clear();
    display.drawString(x, y, text);
    display.display();
}

void setup()
{
    Serial.begin(9600);
    Serial.setTimeout(1000);
  
    display.init();
    display.flipScreenVertically();
    display.setTextAlignment(TEXT_ALIGN_LEFT);
    display.setFont(ArialMT_Plain_10);

    /* Interrupt pins */
    pinMode(int_pin_green, INPUT_PULLUP);
    pinMode(int_pin_red, INPUT_PULLUP);

    /* Setup interrupts */
    attachInterrupt(digitalPinToInterrupt(int_pin_green), int_func_green, LOW);
    attachInterrupt(digitalPinToInterrupt(int_pin_red), int_func_red, LOW);

    timer_red = millis();
    timer_green = millis();
/*
    for (int i = 0; i < 4; i++)
    {
        line[i] = malloc(10);
    }*/

    strcpy(line[0], "Set text");
    strcpy(line[1], "with APP!");
    strcpy(line[2], "\0");
    strcpy(line[3], "\0");
}

void loop()
{
    /* Get command from bluetooth serial */
    char * data;
    data = readSerial();

    setText("Joeläger", 0, 0);

    /* Switch on first char in sent command */
    /*switch (data[0])
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
            /*Serial.println("Connected to voteMachine!");
            Serial.println("GREEN: " + String(vote_green));
            Serial.println("RED: " + String(vote_red));
            break;

        default:
            break;
    }*/

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

        command[command_size] = 0;

        return command;
    }

    return "0";

}



/* Green button interrupt function */
void int_func_green()
{
    if(millis() - timer_green > BUTTON_DELAY)
    {
        vote_green++;
        Serial.println("Green votes: " + String(vote_green));

        sprintf(line[3], "GR: %i RE: %i", vote_green, vote_red);

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

        timer_red = millis();
    }
}
