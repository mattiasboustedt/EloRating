using System;
using System.Collections.Generic;

/**
 * This class calculates ratings based on the Elo system.
 *
 * @author Mattias Boustedt
 * @based on https://github.com/Chovanec/elo-rating by Michael Chovanec
 * @license Creative Commons Attribution 4.0 International License
 */

namespace Rating
{
    public class Rating
    {
        /**
         * FIDE (the World Chess Foundation), gives players with less than 30 played games a K-factor of 25. 
         * Normal players get a K-factor of 15 and pro's get a K-factor of 10.  (Pro = 2400 rating)
         * Once you reach a pro status, you're K-factor never changes, even if your rating drops.
         * 
         * For now set to 15 for everyone.
         */

        private const int KFACTOR = 15;

        public const double WIN = 1;
        public const double LOOSE = 0;
        public const double DRAW = 0.5;

        protected double _ratingA;
        protected double _ratingB;

        protected double _scoreA;
        protected double _scoreB;

        protected double _expectedA;
        protected double _expectedB;

        protected double _newRatingA;
        protected double _newRatingB;

        /**
         * Constructor function which does all the maths and stores the results ready
         * for retrieval.
         *
         * @param double ratingA Current rating of A
         * @param double ratingB Current rating of B
         * @param double scoreA Score of A
         * @param double scoreB Score of B
         */

        public Rating(double ratingA, double ratingB, double scoreA, double scoreB)
        {
            setNewSettings(ratingA, ratingB, scoreA, scoreB);
        }

        /**
        * Set new input data.
        *
        * @param double ratingA Current rating of A
        * @param double ratingB Current rating of B
        * @param double scoreA Score of A
        * @param double scoreB Score of B
        * @return self
        */

        public Rating setNewSettings(double ratingA, double ratingB, double scoreA, double scoreB)
        {
            _ratingA = ratingA;
            _ratingB = ratingB;
            _scoreA = scoreA;
            _scoreB = scoreB;

            List<double> expectedScores = _getExpectedScores(_ratingA, _ratingB);
            _expectedA = expectedScores[0];
            _expectedB = expectedScores[1];

            List<double> newRatingsList = _getNewRatings(_ratingA, _ratingB, _expectedA, _expectedB, _scoreA, _scoreB);
            _newRatingA = newRatingsList[0];
            _newRatingB = newRatingsList[1];

            return this;
        }

        /**
         * Retrieve the calculated data.
         *
         * @return List<double> A list containing the new ratings for A and B.
        */
        public List<double> getNewRatings()
        {
            List<double> newRatingsList = new List<double>
            {
                _newRatingA,
                _newRatingB
            };

            return newRatingsList;
        }

        // Protected & private functions begin here
        /**
         * @param double ratingA The Rating of Player A
         * @param double ratingB The Rating of Player B
         * @return List<double>
         */
        protected List<double> _getExpectedScores(double ratingA, double ratingB)
        {
            double expectedScoreA = 1 / (1 + (Math.Pow(10, ( ratingB - ratingA) / 400)) );
            double expectedScoreB = 1 / (1 + (Math.Pow(10, ( ratingA - ratingB) / 400)) );

            List<double> expectedScoresList = new List<double>
            {
                expectedScoreA,
                expectedScoreB
            };

            return expectedScoresList;
        }

        /**
         * @param double ratingA The Rating of Player A
         * @param double ratingB The Rating of Player A
         * @param double expectedA The expected score of Player A
         * @param double expectedB The expected score of Player B
         * @param double scoreA The score of Player A
         * @param double scoreB The score of Player B
         * @return List<double>
         */
        protected List<double> _getNewRatings(double ratingA, double ratingB, double expectedA, double expectedB, double scoreA, double scoreB)
        {
            double newRatingA = ratingA + (Rating.KFACTOR * ( scoreA - expectedA ) );
            double newRatingB = ratingB + (Rating.KFACTOR * ( scoreB - expectedB ) );

            List<double> newRatingList = new List<double>
            {
                newRatingA,
                newRatingB
            };

            return newRatingList;
        }
    }
}
