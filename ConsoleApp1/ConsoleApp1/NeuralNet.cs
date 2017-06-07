using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class NeuralNet
    {
        System.Random rand = new System.Random();

        private float[][] neuronVal;
        private float[][] neuronBias;
        private float[][][] weights;
        private float[][][] weightsDelta;
        private float[][] biasDelta;
        private float[][] helperDelta;

        private float learningRate = 0.5f;
        private float momentum = 0.1f;

        private int numLayers = 0;
        private int[] numNPerL;

        public float LearningRate
        {
            set
            {
                learningRate = value;
            }
        }

        public float Momentum
        {
            set
            {
                momentum = value;
            }
        }

        public NeuralNet(int[] neuronsPerLayer)
        {
            numLayers = neuronsPerLayer.Length;
            numNPerL = new int[numLayers];

            neuronVal = new float[numLayers][];
            neuronBias = new float[numLayers][];
            biasDelta = new float[numLayers][];
            weights = new float[numLayers][][];
            weightsDelta = new float[numLayers][][];
            helperDelta = new float[numLayers][];
            for (int i = 0; i < numLayers; i++)
            {
                numNPerL[i] = neuronsPerLayer[i];
                neuronVal[i] = new float[neuronsPerLayer[i]];
               neuronBias[i] = new float[neuronsPerLayer[i]];
                biasDelta[i] = new float[neuronsPerLayer[i]];
            }
            for (int i = 1; i < numLayers; i++)
            {
                weights[i] = new float[neuronsPerLayer[i]][];
                weightsDelta[i] = new float[neuronsPerLayer[i]][];
                helperDelta[i] = new float[neuronsPerLayer[i]];
                for (int n = 0; n < neuronsPerLayer[i]; n++)
                {
                    weights[i][n] = new float[neuronsPerLayer[i - 1]];
                    weightsDelta[i][n] = new float[neuronsPerLayer[i - 1]];
                }
            }

            CreateRandomNet();
        }

        public void CreateRandomNet()
        {
            for(int i = 1; i < numLayers; i++)
            {
                for(int n = 0; n < numNPerL[i]; n++)
                {
                    neuronBias[i][n] = RandFloat();
                    helperDelta[i][n] = 0;
                    biasDelta[i][n] = 0;
                    for(int q = 0; q < numNPerL[i - 1]; q++)
                    {
                        weights[i][n][q] = RandFloat();
                        weightsDelta[i][n][q] = 0;
                    }
                }
            }
        }

        public float[] RunNet(float[] inp)
        {
            float[] output = new float[numNPerL[numLayers - 1]];
            float nSum = 0;

            for(int n = 0; n < numNPerL[0]; n++)
            {
                neuronVal[0][n] = inp[n];
            }

            for (int i = 1; i < numLayers; i++)
            {
                for (int n = 0; n < numNPerL[i]; n++)
                {
                    nSum = 0;
                    for (int q = 0; q< numNPerL[i - 1]; q++)
                    {
                        nSum += neuronVal[i - 1][q] * weights[i][n][q];
                    }
                    neuronVal[i][n] = Sigmoid(nSum + neuronBias[i][n]);
                }
            }

            for(int n = 0; n < output.Length; n++)
            {
                output[n] = neuronVal[numLayers - 1][n];
            }
            return output;
        }

        public void Train(float[] target)
        {
            for(int i = numLayers - 1; i > 0; i--)
            {
                for(int n = 0; n < numNPerL[i]; n++)
                {
                    if(i == numLayers - 1)
                    {
                        helperDelta[i][n] = (neuronVal[i][n] - target[n]);
                    }
                    if (i != numLayers - 1)
                    {
                        for (int q = 0; q < numNPerL[i + 1]; q++)
                        {
                            helperDelta[i][n] += helperDelta[i + 1][q] * neuronVal[i + 1][q] * (1f - neuronVal[i + 1][q]) * weights[i + 1][q][n];
                        }
                    }
                }
            }

            for (int i = numLayers - 1; i > 0; i--)
            {
                for (int n = 0; n < numNPerL[i]; n++)
                {
                    biasDelta[i][n] = helperDelta[i][n] * neuronVal[i][n] * (1f - neuronVal[i][n]);
                    neuronBias[i][n] -= learningRate * biasDelta[i][n];
                    for (int q = 0; q < numNPerL[i - 1]; q++)
                    {
                        weights[i][n][q] -= momentum * weightsDelta[i][n][q];
                        weightsDelta[i][n][q] = (helperDelta[i][n] * neuronVal[i][n] * (1f - neuronVal[i][n]) * neuronVal[i - 1][q]);
                        weights[i][n][q] -= learningRate * weightsDelta[i][n][q];
                    }
                    helperDelta[i][n] = 0;
                }
            }
        }

        public float RandFloat()
        {
            return ((float)rand.NextDouble() * 2f) - 1f;
        }

        public float Sigmoid(float x)
        {
            return 1f/(1f + (float)Math.Exp(-x));
        }

        public float SigmoidDeriv(float x)
        {
            return Sigmoid(x) * (1f - Sigmoid(x));
        }

        public  int GetnumLay()
        {
            return neuronVal.Length;
        }
    }
}
