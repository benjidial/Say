// Say v2.0.0, <www.github.com/benjidial/say>

#include <stdio.h>
#include <stdlib.h>
#include <time.h>
#define VERSION "v2.0.0"

enum exitCodes
{
  SUCCESS = 0,
  COMING_SOON = 1,
  INVALID_ARGUMENTS = 2
};

void comingSoon()
{
  printf("This capability is coming soon!\n");
  exit(COMING_SOON);
}

void invArgs(char *message)
{
  fprintf(stderr, "Invalid arguments!\n%s\nDo 'Say --help' for help.\n", message);
  exit(INVALID_ARGUMENTS);
}

void help()
{
  printf("Say syntax:\n\n");
  printf("Say --help\n");
  printf("Say --license\n");
  printf("Say --version\n");
  printf("Say [-i=interval] -f=filename\n");
  printf("Say [-i=interval] -t=text\n\n");
  printf("  --help     Prints this screen\n");
  printf("  --license  Prints lincense information\n");
  printf("  --version  Provides version and project info\n\n");
  printf("  interval   The time in milliseconds between each character\n");
  printf("             If not specified, 750 ms is used.\n");
  printf("  filename   A file the contents of which to output\n");
  printf("  text       Text to output\n\n\n");
  printf("Return values of Say:\n\n");
  printf("0:   Success\n");
  printf("1:   Coming soon\n");
  printf("2:   Invalid arguments\n");
  exit(SUCCESS);
}

void license()
{
  printf("Say is licensed under CC0 1.0 Universal.\n");
  printf("You should have gotten a file named 'License.txt' with Say.\n");
  printf("For more information, visit <creativecommons.org/publicdomain/zero/1.0>.\n");
  exit(SUCCESS);
}

void version()
{
  printf("Say, %s.\n", VERSION); // See #define near the top of this file.
  printf("<github.com/benjidial/say>\n");
  exit(SUCCESS);
}

int main(int argc, char *argv[])
{
  if (argc == 1)
    invArgs("Say must have at least one argument.");
  if (argc == 2)
  {
    if (argv[1][0] == '-')
    switch (argv[1][1])
    {
      case '-':
        if (!strcmp(argv[1], "--help"))
          help();
        if (!strcmp(argv[1], "--license"))
          license();
        if (!strcmp(argv[1], "--version"))
          version();
        char *message;
        sprintf(message, "I don't recognize %s in this context.", argv[1]);
        invArgs(message);
      case 'f':
        comingSoon();
      case 'i':
        comingSoon();
      case 't':
        comingSoon();
      default:
        comingSoon();
    }
  }
  comingSoon();
}
