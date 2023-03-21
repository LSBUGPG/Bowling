using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class Scoring
{
    public static void Score(Rolls rolls, Scorecard scorecard)
    {
        int frameScore = 0, prevFrame = 0, prevFrameTwo = 0, bowlOne, bowlTwo = 0, frame = 1, totalScore = 0, extraFrame;
        bool strike = false, strikeTwo = false, spare = false;

        for (; frame <= 10; frame++)
        {
            bowlOne = rolls.NextRoll();
            if (spare == true)// if previous frame was a spare add in the extra points now
            {
                prevFrame = 10 + bowlOne;
                spare = false;
                totalScore = prevFrame + totalScore;
                scorecard.ScoreFrame(totalScore);
            }
            if (strikeTwo == true && bowlOne == 10)
            {
                prevFrameTwo = 30;
                totalScore = prevFrameTwo + totalScore;
                scorecard.ScoreFrame(totalScore);
            }
            if (strikeTwo == true && bowlOne != 10)
            {
                strikeTwo = false;
                prevFrameTwo = 10 + 10 + bowlOne;
                totalScore = prevFrameTwo + totalScore;
                scorecard.ScoreFrame(totalScore);
            }
            if (strike == true && bowlOne == 10)
            {
                strikeTwo = true;
                prevFrameTwo = 20;
            }

            if (bowlOne < 10) //check to make sure there wasn't a strike on first bowl
            {
                bowlTwo = rolls.NextRoll();
                if (bowlOne + bowlTwo == 10)
                {
                    spare = true;
                    scorecard.MarkSpare(bowlOne);
                }

                if (strikeTwo == true && frame == 10)
                {
                    prevFrameTwo = 10 + 10 + bowlTwo;
                    totalScore = prevFrameTwo + totalScore;
                    scorecard.ScoreFrame(totalScore);
                    strikeTwo = false;
                }

                if (strike == true && bowlOne != 10)
                {
                    strike = false;
                    prevFrame = 10 + bowlOne + bowlTwo;
                    totalScore = totalScore + prevFrame;
                    scorecard.ScoreFrame(totalScore);
                }
                if (spare != true && strike != true && strikeTwo != true)
                {
                    frameScore = bowlOne + bowlTwo;
                    totalScore = totalScore + frameScore;
                    scorecard.MarkOpen(bowlOne, bowlTwo);
                    scorecard.ScoreFrame(totalScore);
                }
            }
            else
            {
                strike = true;
                prevFrame = 10;
                if (frame != 10)
                    scorecard.MarkStrike();
            }
            if (frame == 10 && strike == true)
            {
                bowlTwo = rolls.NextRoll();
                if (strikeTwo == true)
                {
                    prevFrameTwo = 10 + 10 + bowlTwo;
                    totalScore = prevFrameTwo + totalScore;
                    scorecard.ScoreFrame(totalScore);
                    strikeTwo = false;
                }
            }

            if (frame == 10 && (spare == true || strike == true))
            {
                extraFrame = rolls.NextRoll();
                if (strike == true)
                {
                    prevFrame = 10 + bowlTwo + extraFrame;
                    totalScore = totalScore + prevFrame;
                    scorecard.ScoreFrame(totalScore);
                    if (bowlTwo == 10 && extraFrame == 10)
                    {
                        scorecard.MarkBonusStrikes();
                    }
                    else if (bowlTwo == 10)
                    {
                        scorecard.MarkBonusStrike(extraFrame);
                    }
                    else if (bowlTwo + extraFrame == 10)
                    {
                        scorecard.MarkBonusSpare(bowlTwo);
                    }
                    else
                    {
                        scorecard.MarkBonusBalls(bowlTwo, extraFrame);
                    }
                }
                else
                {
                    if (extraFrame == 10)
                    {
                        scorecard.MarkBonusStrike();
                    }
                    else
                    {
                        scorecard.MarkBonusBall(extraFrame);
                    }
                    totalScore = totalScore + 10 + extraFrame;
                    scorecard.ScoreFrame(totalScore);
                }
            }
        }
    }
}
