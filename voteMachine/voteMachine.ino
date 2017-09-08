#include <U8glib.h>

/* Command buffer size / length */
#define SERIAL_BUFFER_SIZE 30
#define LINE_HEIGHT 15
#define BUTTON_TIME 1000

const byte ledPin = 8;
const byte interruptPinGreen = 2;
const byte interruptPinRed = 3;

unsigned long timerYes;
unsigned long timerNo;

const char COMMAND_END = '\n';
U8GLIB_SSD1306_128X64 display(U8G_I2C_OPT_NONE);

char * line[4];

int yes, no;


void yesFunc()
{
    if(millis() - timerYes > BUTTON_TIME)
    {
        yes++;
        Serial.println("Yes votes: " + String(yes));

        sprintf(line[3], "YES: %i NO: %i", yes, no);
        setText();

        timerYes = millis();
    }
}

void noFunc()
{
    if(millis() - timerNo > BUTTON_TIME)
    {
        no++;
        Serial.println("No votes: " + String(no));

        sprintf(line[3], "YES: %i NO: %i", yes, no);
        setText();

        timerNo = millis();
    }
}

void setup()
{
    Serial.begin(9600);
    Serial.setTimeout(1000);

    pinMode(ledPin, OUTPUT);
    pinMode(interruptPinGreen, INPUT_PULLUP);
    pinMode(interruptPinRed, INPUT_PULLUP);
    attachInterrupt(digitalPinToInterrupt(interruptPinGreen), yesFunc, LOW);
    attachInterrupt(digitalPinToInterrupt(interruptPinRed), noFunc, LOW);

    timerNo = millis();
    timerYes = millis();

    for (int i = 0; i < 4; i++)
    {
        line[i] = malloc(10);
    }
}

void loop()
{
    /* Get command from bluetooth serial */
    char * data;
    data = readSerial();

    /* Switch on first char in sent command */
    switch (data[0])
    {
        case '1':
            strcpy(line[0], data + 1);
            setText();
            break;

        case '2':
            strcpy(line[1], data + 1);
            setText();
            break;

        case '3':
            strcpy(line[2], data + 1);
            setText();
            break;

        case '4':
            strcpy(line[3], data + 1);
            setText();
            break;

        case 'C':
            for (int i = 0; i < 4; i++)
            {
                strcpy(line[i], "\0");
            }

            yes = 0;
            no = 0;

            setText();

            break;

        case 'S':
            Serial.println("Connected to voteMachine!");
            break;

        default:
            break;
    }

    //delay(1);
}


/* Read serial command (from bluetooth) */
char * readSerial()
{
    char command[SERIAL_BUFFER_SIZE + 1];

    int size = Serial.readBytesUntil(
        COMMAND_END,
        command,
        SERIAL_BUFFER_SIZE
    );

    if(size > 0)
    {
        /* Store command size as string */
        sprintf(line[3], "COMMAND SIZE: %i", size);
        setText();

        command[size] = 0;

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
        /* Använd 3 globala strängar för rader? */
        display.setFont(u8g_font_helvB10);

        display.drawStr(0, 1 * LINE_HEIGHT, line[0]);
        display.drawStr(0, 2 * LINE_HEIGHT, line[1]);
        display.drawStr(0, 3 * LINE_HEIGHT, line[2]);
        display.drawStr(0, 4 * LINE_HEIGHT, line[3]);
    }
    while(display.nextPage());

}
