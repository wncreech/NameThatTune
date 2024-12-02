# NameThatTune
Console app based on the classic games show

# Name That Tune
Inspired by a favorite daytime television show of my grandmother's, this console app generates a short listening quiz to test how quickly you can recognize music (and indirectly, how quickly you can type). Music (sourced from Deezer API's 30-second song previews) can be sourced from any public Spotify playlist and high scores per playlist are saved between games. Compete with your friends to gloat about your superior taste and knowledge of music!

# How to Play
-ensure internet connection

-UI designed for Windows Command Prompt

-Only links for public Spotify playlists will work to generate playlists. A Spotify subscription is not required to access these online. 

-clone repositories, install NuGet dependencies (SpotifyAPI and NAudio), etc.


# Notes

-NuGet Packages Used: SpotifyAPI-NET, NAudio

-I made a concentrated effort to own this project and rely on my in-class knowledge as much as possible. However, many aspects, especially the API authorization aspects, required some independent research. 

-Spotify's [recent API changes](https://developer.spotify.com/blog/2024-11-27-changes-to-the-web-api), specifically the restriction on preview URLs, broke the first app entirely and almost derailed the whole project. I figured out a solid alternative using Deezer's search API, but was unable to gain authorization to previews of certain artists/labels like I would have been on Spotify. This means two things for the user:

    -songs from your Spotify playlists may go to the next-nearest version - an unavailable live version may be replaced with a studio recording, for example. Any substitutions should almost always be from the same artist, however.
    -a select few artists' work are entirely unavailable in Deezer's song previews, and these songs are not playable in the game. The most notable exclusion is Taylor Swift's entire discography. My sincerest apologies to any fans of hers - I'm  working on it.

On that note, there is a significant amount of unused code here, especially for the front end, which I decided would be better off simpler for now. I have learned valuable lessons about the dangers of tight coupling and bad practices and plan to refactor Heavily in this project's next steps!



# Rules
-the player is scored based on how quickly they answer each round. the maximum points that can be earned in one round is 3000, although that would require an almost instantaneous response

-wrong answers are met with a delay that costs the player ~200 points

-the player has 30 seconds to answer each question and will score 0 for the round if out of time or skipped

-guesses are not case sensitive, ignore white space entirely, and observe no punctuation, allowing for easy typing

# Next Steps
-refine Deezer sample matching

-build a legitimate front end

-add support for playlists from other streaming services


