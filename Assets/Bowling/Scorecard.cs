using System;
using System.Diagnostics;
using System.Linq;
using System.Text;

public class Scorecard
{
    string name;
    string marks;
    int [] scores;
    int currentFrame;

    public Scorecard(string name)
    {
        this.name = name;
        marks = "";
        scores = new int[10];
        currentFrame = 0;
    }

    public Scorecard(string name, string marks, int[] scores)
    {
        this.name = name;
        this.marks = marks;
        this.scores = scores;
        currentFrame = 0;
    }

    char ScoreToMark(int score)
    {
        return "-123456789"[score];
    }

    void AddMarks(string marks)
    {
        this.marks += marks;
    }

    public void MarkSpare(int first)
    {
        AddMarks($"{ScoreToMark(first)}/");
    }

    public void MarkOpen(int first, int second)
    {
        AddMarks($"{ScoreToMark(first)}{ScoreToMark(second)}");
    }

    public void MarkStrike()
    {
        AddMarks("X");
    }

    public void MarkBonusStrikes()
    {
        AddMarks("XXX");
    }

    public void MarkBonusStrike(int bonus)
    {
        AddMarks($"XX{ScoreToMark(bonus)}");
    }

    public void MarkBonusSpare(int first)
    {
        AddMarks($"X{ScoreToMark(first)}/");
    }

    public void MarkBonusBalls(int first, int second)
    {
        AddMarks($"X{ScoreToMark(first)}{ScoreToMark(second)}");
    }

    public void MarkBonusStrike()
    {
        AddMarks("X");
    }

    public void MarkBonusBall(int first)
    {
        AddMarks($"{ScoreToMark(first)}");
    }

    public void ScoreFrame(int score)
    {
        scores[currentFrame] = score;
        currentFrame++;
    }

    string ListMarks()
    {
        return marks;
    }

    string ListScores()
    {
        return string.Join(" ", scores);
    }

    public override string ToString()
    {
        return $"Scorecard {name} marks {ListMarks()} scores {ListScores()}";
    }

    public override bool Equals(object obj)
    {
        return (obj is Scorecard)? Equals(obj as Scorecard) : base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public bool Equals(Scorecard other)
    {
        return name == other.name && marks == other.marks && Enumerable.SequenceEqual(scores, other.scores);
    }

    public void Display(Display display)
    {
        display.player.text = name;
        int mark = 0;
        for (int frame = 1; frame <= 10; frame++)
        {
            var displayFrame = display.frames[frame - 1];
            int marksThisFrame = 2;
            bool strike = marks[mark] == 'X';
            if (strike)
            {
                marksThisFrame = 1;
            }
            if (frame == 10)
            {
                if (strike || marks[mark + 1] == '/')
                {
                    marksThisFrame = 3;
                }
            }
            for (int i = 0; i < marksThisFrame; ++i)
            {
                if (marksThisFrame == 1)
                {
                    displayFrame.marks[1].text = marks[mark + i].ToString();
                }
                else
                {
                    displayFrame.marks[i].text = marks[mark + i].ToString();
                }
            }
            mark += marksThisFrame;
            displayFrame.subTotal.text = scores[frame - 1].ToString();
        }
    }
}
