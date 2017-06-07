using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class TrainingData
    {

        public enum Gates
        {
            AND,
            OR,
            XOR
        };

        public string GATEtest(Gates gateType, int trainingIter, int numHiddenN, float learningRate, float momentum)
        {
            NeuralNet network = new NeuralNet(new int[3] { 2, numHiddenN, 1 });

            network.LearningRate = learningRate;
            network.Momentum = momentum;

            float[][] inData = new float[4][];
            float[][] targData = new float[4][];
            float[][] outData = new float[4][];

            string outputString = "";

            for (int i = 0; i < 4; i++)
            {
                inData[i] = new float[2];
                targData[i] = new float[1];
                outData[i] = new float[1];
            }

            inData[0][0] = 0;
            inData[0][1] = 0;
            inData[1][0] = 0;
            inData[1][1] = 1;
            inData[2][0] = 1;
            inData[2][1] = 0;
            inData[3][0] = 1;
            inData[3][1] = 1;

            switch (gateType)
            {
                case Gates.AND:
                    outputString += "AND ";

                    targData[0][0] = 0f;
                    targData[1][0] = 0f;
                    targData[2][0] = 0f;
                    targData[3][0] = 1f;
                    break;
                case Gates.OR:
                    outputString += "OR ";

                    targData[0][0] = 0f;
                    targData[1][0] = 1f;
                    targData[2][0] = 1f;
                    targData[3][0] = 1f;
                    break;
                case Gates.XOR:
                    outputString += "XOR ";

                    targData[0][0] = 0f;
                    targData[1][0] = 1f;
                    targData[2][0] = 1f;
                    targData[3][0] = 0f;
                    break;
            }

            outputString += "GATE TEST  \n";

            for (int i = 0; i < trainingIter; i++)
            {
                for (int n = 0; n < 4; n++)
                {
                    outData[n] = network.RunNet(inData[n]);
                    network.Train(targData[n]);
                }
            }

            for(int i = 0; i < 4; i++)
            {
                outputString += inData[i][0] + ":" + inData[i][1] + "  =  " + outData[i][0] + "\n";
            }

            return outputString;
        }
        
        public string BINARYtest(int trainingIter, int numHiddenN, float learningRate, float momentum)
        {
            NeuralNet network = new NeuralNet(new int[3] { 3, numHiddenN, 8 });

            network.LearningRate = learningRate;
            network.Momentum = momentum;

            string outputString = "BINARY TEST \n";

            float[][] inData = new float[8][];
            float[][] targData = new float[8][];
            float[][] outData = new float[8][];

            for (int i = 0; i < 8; i++)
            {
                inData[i] = new float[3];
                targData[i] = new float[8];
                outData[i] = new float[8];
            }

            inData[0][0] = 0; inData[0][1] = 0; inData[0][2] = 0;
            inData[1][0] = 0; inData[1][1] = 0; inData[1][2] = 1;
            inData[2][0] = 0; inData[2][1] = 1; inData[2][2] = 0;
            inData[3][0] = 0; inData[3][1] = 1; inData[3][2] = 1;
            inData[4][0] = 1; inData[4][1] = 0; inData[4][2] = 0;
            inData[5][0] = 1; inData[5][1] = 0; inData[5][2] = 1;
            inData[6][0] = 1; inData[6][1] = 1; inData[6][2] = 0;
            inData[7][0] = 1; inData[7][1] = 1; inData[7][2] = 1;

            for(int i = 0; i < 8; i++)
            {
                for(int n = 0; n < 8; n++)
                {
                    targData[i][n] = (i == n) ? 1 : 0;
                }
            }

            for(int i = 0; i < trainingIter; i++)
            {
                for(int n = 0; n < 8; n++)
                {
                    outData[n] = network.RunNet(inData[n]);
                    network.Train(targData[n]);
                }
            }

            for(int i = 0; i < 8; i++)
            {
                outputString += inData[i][0] + ":" + inData[i][1] + ":" + inData[i][2] + " = ";
                for(int n = 0; n < 8; n++)
                {
                    outputString += outData[i][n] + " : ";
                }
                outputString += "\n";
            }
            return outputString;
        }


    }
}
