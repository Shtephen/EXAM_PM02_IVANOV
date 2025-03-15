using System;
using System.IO;
using System.Linq;
using Xunit;


public class ProgramTests
{
    [Fact]
    public void GetStartPoint_ValidInput_ReturnsCorrectStartPoint()
    {
        
        using (var sw = new StringWriter())
        {
            Console.SetOut(sw);
            using (var sr = new StringReader("5\n")) 
            {
                Console.SetIn(sr);

               
                int startPoint = Program.GetStartPoint();

                
                Assert.Equal(4, startPoint); 
            }
        }
    }

    

    [Fact]
    public void GetInterestedPoints_InvalidInput_PromptsAgain()
    {
        
        using (var sw = new StringWriter())
        {
            Console.SetOut(sw);
            
            using (var sr = new StringReader("InvalidInput\n3, 11, 4\n2, 4, 6\n"))
            {
                Console.SetIn(sr);

                // Act
                int[] points = Program.GetInterestedPoints();

                // Assert
                Assert.Equal(new int[] { 1, 3, 5 }, points); 
            }
        }
    }
}