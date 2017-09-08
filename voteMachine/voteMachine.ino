#include <U8glib.h>

#define SERIAL_BUFFER_SIZE 30
#define LINE_HEIGHT 15
#define BUTTON_DELAY 100
const char COMMAND_END = '\n';

const byte int_pin_green = 2;
const byte int_pin_red = 3;

unsigned long timer_green;
unsigned long timer_red;

U8GLIB_SSD1306_128X64 display(U8G_I2C_OPT_NONE);

char * line[4];
int vote_green, vote_red;

void setup()
{
    Serial.begin(9600);
    Serial.setTimeout(1000);

    /* Display font */
    display.setFont(u8g_font_helvB10);

    /* Interrupt pins */
    pinMode(int_pin_green, INPUT_PULLUP);
    pinMode(int_pin_red, INPUT_PULLUP);

    /* Setup interrupts */
    attachInterrupt(digitalPinToInterrupt(int_pin_green), int_func_green, LOW);
    attachInterrupt(digitalPinToInterrupt(int_pin_red), int_func_red, LOW);

    timer_red = millis();
    timer_green = millis();

    for (int i = 0; i < 4; i++)
    {
        line[i] = malloc(10);
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
            Serial.prinln("Line 1 set: " + String(data + 1));
            break;

        case '2': // Line 2
            strcpy(line[1], data + 1);
            setText();
            break;

        case '3': // Line 3
            strcpy(line[2], data + 1);
            setText();
            break;

        case '4': // Line 4
            strcpy(line[3], data + 1);
            setText();
            break;

        case 'C': // Clear
            for (int i = 0; i < 4; i++)
            {
                strcpy(line[i], "\0");
            }

            vote_red = 0;
            vote_green = 0;

            setText();

            break;

        case 'S': // Status
            Serial.println("Connected to voteMachine!");
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
        /* Store command size as string */
        sprintf(line[3], "CMD SIZE: %i", command_size);
        setText();

        command[command_size] = 0;

        return command;
    }

    return "0";

}

/* Write text to display */
void setText()
{
    display.firstPage();

    do
    {
        display.drawStr(0, 1 * LINE_HEIGHT, line[0]);
        display.drawStr(0, 2 * LINE_HEIGHT, line[1]);
        display.drawStr(0, 3 * LINE_HEIGHT, line[2]);
        display.drawStr(0, 4 * LINE_HEIGHT, line[3]);
    }
    while(display.nextPage());

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
