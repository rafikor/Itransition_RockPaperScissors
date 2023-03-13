# Generalized game rock-paper-scissors game

This project implements generalized rock-paper-scissors game (with the supports of arbitrary odd number of arbitrary combinations).

The code generates a cryptographically strong random key (RandomNumberGenerator) with a length of 256 bits, makes computes move, calculates HMAC (based on SHA3) from the own move with the generated key, displayed the HMAC to the user. After that the user gets "menu". The user makes his choice. The code shows who won, the move of the computer and the original key.

Terminal.Gui is used for building of UI, see https://gui-cs.github.io/Terminal.Gui/index.html 