# EloRating C#
The Elo rating system is a method for calculating the relative skill levels of players in competitor-versus-competitor games such as chess. 
<br /> 
<br />
Read more at [Wikipedia](http://en.wikipedia.org/wiki/Elo_rating_system).

# Usage

            Rating rating = new Rating(1020, 1000, Rating.LOOSE, Rating.WIN);
            List<double> newRatings = rating.GetNewRatings();

# Credits
Based on [Elo Rating PHP](https://github.com/Chovanec/elo-rating) by [Michal Chovanec](http://michalchovanec.com).

# License
<span xmlns:dct="http://purl.org/dc/terms/" property="dct:title">Elo Rating C#</span> by Mattias Boustedt is licensed under a <a rel="license" href="http://creativecommons.org/licenses/by/4.0/">Creative Commons Attribution 4.0 International License</a>.
