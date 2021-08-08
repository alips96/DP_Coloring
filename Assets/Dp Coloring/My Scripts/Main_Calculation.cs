using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Dp_Coloring
{
	public class Main_Calculation : MonoBehaviour 
	{
        GameManager_Master gameManagerMaster;
        string symmetricalBlock;

        private void OnEnable()
        {
            setInitialRefrences();
            gameManagerMaster.EventStartCalculation += StartMainCalculation;
        }

        private void OnDisable()
        {
            gameManagerMaster.EventStartCalculation -= StartMainCalculation;
        }

        private void setInitialRefrences()
        {
            gameManagerMaster = GetComponent<GameManager_Master>();
        }

        private void StartMainCalculation(StringBuilder block,int k)
        {
            symmetricalBlock = CalculateAndReturnSymmetricalList(block, k - 1);

            gameManagerMaster.CallEventDisplayResult(symmetricalBlock); //Invoke Display Event!
        }

        public string CalculateAndReturnSymmetricalList(StringBuilder block, int kPosition)
        {
            StringBuilder leftStringBuilder = new StringBuilder();
            StringBuilder rightStringBuilder = new StringBuilder();

            //assignment of left and right parts of the block
            for (int i = 0; i < block.Length; i++)
            {
                if(i <= kPosition)
                {
                    leftStringBuilder.Append(block[i]);
                }
                else
                {
                    rightStringBuilder.Append(block[i]);
                }
            }

            string leftString = leftStringBuilder.ToString();
            string rightString = rightStringBuilder.ToString();
            
            int[,] num = new int[leftString.Length, rightString.Length]; //num is the identifier matrix of common substrings

            int longestCommonSubstringLength = LongestCommonSubstring(leftString, rightString,out num);

            //Start symmetrifying
            List<int> valuableIndexes = FillOutListIndexes(num,leftString,rightString);

            //Filling out constraints
            HashSet<int> leftIndexesConstraintsSet = new HashSet<int>();
            HashSet<int> rightIndexesConstraintsSet = new HashSet<int>();

            for (int i = 0; i < valuableIndexes.Count; i++)
            {
                string currentValueString = valuableIndexes[i].ToString();

                rightIndexesConstraintsSet.Add(int.Parse(currentValueString[currentValueString.Length - 1].ToString()));
                leftIndexesConstraintsSet.Add(int.Parse(currentValueString[currentValueString.Length - 2].ToString()));
                rightIndexesConstraintsSet.Add(int.Parse(currentValueString[currentValueString.Length - 3].ToString()));
                leftIndexesConstraintsSet.Add(int.Parse(currentValueString[currentValueString.Length - 4].ToString()));
            }

            //Last step
            StringBuilder returnStringBuilder = new StringBuilder();
            returnStringBuilder = SymmetrifyLeftBlock(leftStringBuilder, rightStringBuilder, valuableIndexes, rightIndexesConstraintsSet, leftIndexesConstraintsSet);

            return returnStringBuilder.Append(rightStringBuilder).ToString();
        }

        public StringBuilder SymmetrifyLeftBlock(StringBuilder leftStringBuilder, StringBuilder rightStringBuilder, List<int> valuableIndexes, HashSet<int> rightIndexesConstraintsSet, HashSet<int> leftIndexesConstraintsSet)
        {
            int operationCounter = 0;

            Dictionary<int, StringBuilder> beforeInsertHandler = new Dictionary<int, StringBuilder>();
            Dictionary<int, StringBuilder> afterInsertHandler = new Dictionary<int, StringBuilder>();
            Dictionary<int, List<int>> lostSubStringsHandlerDic = new Dictionary<int, List<int>>();

            for (int i = 0; i < valuableIndexes.Count; i++)
            {
                string currentIndex = valuableIndexes[i].ToString();
                int startRightBlockIndex = int.Parse(currentIndex[currentIndex.Length - 1].ToString());
                int startLeftBlockIndex = int.Parse(currentIndex[currentIndex.Length - 2].ToString());
                int endRightBlockIndex = int.Parse(currentIndex[currentIndex.Length - 3].ToString());
                int endLeftBlockIndex = int.Parse(currentIndex[currentIndex.Length - 4].ToString());

                if (!lostSubStringsHandlerDic.ContainsKey(startLeftBlockIndex))
                {
                    lostSubStringsHandlerDic.Add(startLeftBlockIndex, new List<int> { startRightBlockIndex, endRightBlockIndex });
                }

                if (!lostSubStringsHandlerDic.ContainsKey(endLeftBlockIndex))
                {
                    lostSubStringsHandlerDic.Add(endLeftBlockIndex, new List<int> { startRightBlockIndex, endRightBlockIndex });
                }
                
            }

            for (int i = 0; i < valuableIndexes.Count; i++) //main loop
            {
                string currentIndex = valuableIndexes[i].ToString();

                int startRightBlockIndex = int.Parse(currentIndex[currentIndex.Length - 1].ToString());
                int startLeftBlockIndex = int.Parse(currentIndex[currentIndex.Length - 2].ToString());
                int endRightBlockIndex = int.Parse(currentIndex[currentIndex.Length - 3].ToString());
                int endLeftBlockIndex = int.Parse(currentIndex[currentIndex.Length - 4].ToString());

                //check if this substring is lost
                if(leftStringBuilder[startLeftBlockIndex] == 'E')
                {
                    continue;
                }

                //left measurement
                int j = 1;
                int indexConstaint = 100;

                while (startRightBlockIndex - j >= 0 && !rightIndexesConstraintsSet.Contains(startRightBlockIndex - j))
                {
                    if(startLeftBlockIndex - j < 0 || leftIndexesConstraintsSet.Contains(startLeftBlockIndex - j))
                    {
                        if(indexConstaint == 100)
                        {
                            indexConstaint = startLeftBlockIndex - j + 1;
                            beforeInsertHandler.Add(startLeftBlockIndex - j + 1, new StringBuilder(rightStringBuilder[startRightBlockIndex - j].ToString()));
                            operationCounter++;
                        }
                        else
                        {
                            beforeInsertHandler[indexConstaint].Insert(0, rightStringBuilder[startRightBlockIndex - j].ToString(), 1);
                            operationCounter++;
                        }             
                    }
                    else
                    {
                        leftStringBuilder[startLeftBlockIndex - j] = rightStringBuilder[startRightBlockIndex - j]; //change color
                        operationCounter++;
                    }
                    rightIndexesConstraintsSet.Add(startRightBlockIndex - j);
                    j++;
                }

                if(startRightBlockIndex - j < 0)
                {
                    for (int p = 0; p < startLeftBlockIndex - j + 1; p++)
                    {
                        if (leftIndexesConstraintsSet.Contains(p))
                            leftIndexesConstraintsSet.Remove(p);
                        
                        if (lostSubStringsHandlerDic.ContainsKey(p)) //found a substring that needs to be deleted
                        {
                            foreach (int item in lostSubStringsHandlerDic[p])
                            {
                                if (rightIndexesConstraintsSet.Contains(item))
                                {
                                    rightIndexesConstraintsSet.Remove(item);
                                }
                            }
                            lostSubStringsHandlerDic.Remove(p);
                        }

                        if (beforeInsertHandler.ContainsKey(p))
                        {
                            for (int m = 0; m < beforeInsertHandler[p].Length; m++)
                            {
                                operationCounter++; //delete
                            }

                            beforeInsertHandler.Remove(p);
                        }

                        if(p != startLeftBlockIndex - 1)
                        {
                            if (afterInsertHandler.ContainsKey(p))
                            {
                                for (int q = 0; q < afterInsertHandler[p].Length; q++)
                                {
                                    operationCounter++;
                                }

                                afterInsertHandler.Remove(p);
                            }
                        }

                        leftStringBuilder[p] = 'E'; //delete
                        operationCounter++;
                    }
                }

                //Right measurement
                j = 1;
                indexConstaint = 100;

                while (endRightBlockIndex + j < rightStringBuilder.Length && !rightIndexesConstraintsSet.Contains(endRightBlockIndex + j))
                {
                    if (endLeftBlockIndex + j >= leftStringBuilder.Length || leftIndexesConstraintsSet.Contains(endLeftBlockIndex + j))
                    {
                        if(indexConstaint == 100)
                        {
                            indexConstaint = endLeftBlockIndex + j - 1;
                            afterInsertHandler.Add(endLeftBlockIndex + j - 1, new StringBuilder(rightStringBuilder[endRightBlockIndex + j].ToString()));
                            operationCounter++;
                        }
                        else
                        {
                            afterInsertHandler[indexConstaint].Append(rightStringBuilder[endRightBlockIndex + j]);
                            operationCounter++;
                        }
                    }
                    else
                    {
                        leftStringBuilder[endLeftBlockIndex + j] = rightStringBuilder[endRightBlockIndex + j]; //change color
                        operationCounter++;
                    }
                    rightIndexesConstraintsSet.Add(endRightBlockIndex + j);
                    j++;
                }

                if (endRightBlockIndex + j >= rightStringBuilder.Length)
                {
                    for (int p = endLeftBlockIndex + j; p < leftStringBuilder.Length; p++) //hey be carefuuuul j instead of 1
                    {
                        if (leftIndexesConstraintsSet.Contains(p))
                            leftIndexesConstraintsSet.Remove(p);

                        if (lostSubStringsHandlerDic.ContainsKey(p)) // found a substring that needs to be deleted
                        {
                            foreach (int item in lostSubStringsHandlerDic[p])
                            {
                                if (rightIndexesConstraintsSet.Contains(item))
                                {
                                    rightIndexesConstraintsSet.Remove(item);
                                }
                            }
                            lostSubStringsHandlerDic.Remove(p);
                        }

                        if (beforeInsertHandler.ContainsKey(p))
                        {
                            for (int m = 0; m < beforeInsertHandler[p].Length; m++)
                            {
                                operationCounter++; //delete
                            }

                            beforeInsertHandler.Remove(p);
                        }

                        if (p != leftStringBuilder.Length - 1 && afterInsertHandler.ContainsKey(p))
                        {
                            for (int m = 0; m < afterInsertHandler[p].Length; m++)
                            {
                                operationCounter++; //delete
                            }
                            afterInsertHandler.Remove(p);
                        }

                        leftStringBuilder[p] = 'E'; //delete
                        operationCounter++;
                    }
                }
            }

            //last step
            StringBuilder finalLeftStringBuilder = new StringBuilder();

            if(valuableIndexes.Count > 0)
            {
                for (int i = 0; i < leftStringBuilder.Length; i++)
                {
                    if (beforeInsertHandler.ContainsKey(i))
                    {
                        finalLeftStringBuilder.Append(beforeInsertHandler[i]);
                    }

                    if (leftStringBuilder[i] != 'E')
                    {
                        finalLeftStringBuilder.Append(leftStringBuilder[i]);
                    }

                    if (afterInsertHandler.ContainsKey(i))
                    {
                        finalLeftStringBuilder.Append(afterInsertHandler[i]);
                    }
                }

                for (int i = 0; i < rightStringBuilder.Length; i++)
                {
                    if (finalLeftStringBuilder[i] != rightStringBuilder[i])
                    {
                        finalLeftStringBuilder.Insert(i, rightStringBuilder[i]);
                        operationCounter++;
                    }
                }
            }
            else
            {
                for (int i = 0; i < rightStringBuilder.Length; i++)
                {
                    if (i < leftStringBuilder.Length)
                    {
                        if (leftStringBuilder[i] != rightStringBuilder[i])
                        {
                            leftStringBuilder[i] = rightStringBuilder[i]; //assign
                            operationCounter++;
                        }
                    }
                    else
                    {
                        leftStringBuilder.Append(rightStringBuilder[i]);
                        operationCounter++;
                    }

                    if (i == rightStringBuilder.Length - 1)
                    {
                        int countOfElementsToBeRemoved = leftStringBuilder.Length - i - 1;
                        leftStringBuilder.Remove(i + 1, countOfElementsToBeRemoved);
                        operationCounter += countOfElementsToBeRemoved;
                    }
                }

                finalLeftStringBuilder.Append(leftStringBuilder);
            }

            finalLeftStringBuilder.Append(operationCounter.ToString());
            return finalLeftStringBuilder;
        }

        public List<int> FillOutListIndexes(int[,] num, string leftString, string rightString)
        {
            LinkedList<int> valuableLinkedList = new LinkedList<int>();
            List<int> returnList = new List<int>();
            HashSet<int> verticalIndexConstraints = new HashSet<int>();
            HashSet<int> horizontalIndexConstraints = new HashSet<int>();

            for (int i = 0; i < leftString.Length; i++)
            {
                for (int j = 0; j < rightString.Length; j++)
                {
                    if (num[i, j] == 2)
                    {
                        if (!verticalIndexConstraints.Contains(i - 1))
                            verticalIndexConstraints.Add(i - 1);

                        num[i - 1, j - 1] = 0; //To simplify subsequent calculation

                        if (i - 2 >= 0)
                        {
                            num[i - 2, j - 1] = 0; //To simplify subsequent calculation
                        }

                        if (!valuableLinkedList.Contains(2))
                        {
                            valuableLinkedList.AddFirst(2);
                        }

                        int valuableSubStringi = i;
                        int valuableSubStringj = j;
                        int currentNode = 2;
                        int i2 = i + 1;
                        int j2 = j + 1;

                        while (i2 < leftString.Length && j2 < rightString.Length)
                        {
                            if (num[i2, j2] == currentNode + 1)
                            {
                                if (!verticalIndexConstraints.Contains(i2))
                                    verticalIndexConstraints.Add(i2);

                                if (!horizontalIndexConstraints.Contains(j2))
                                    horizontalIndexConstraints.Add(j2);

                                valuableSubStringi = i2;
                                valuableSubStringj = j2;
                            }
                            else
                                break;
                            i2++;
                            j2++;
                            currentNode++;
                        }

                        if (!verticalIndexConstraints.Contains(i) && !horizontalIndexConstraints.Contains(j))
                        {
                            if (!valuableLinkedList.Contains(currentNode))
                            {
                                valuableLinkedList.AddAfter(valuableLinkedList.Find(2), currentNode);
                            }

                            valuableLinkedList.AddBefore(valuableLinkedList.Find(currentNode), ((j - 1) + ((i - 1) * 10) + (valuableSubStringj * 100) + (valuableSubStringi * 1000) + (10000)));
                        }

                        if (!verticalIndexConstraints.Contains(i))
                            verticalIndexConstraints.Add(i);

                        if (!horizontalIndexConstraints.Contains(j))
                            horizontalIndexConstraints.Add(j);
                    }
                }
            }

            //one_length_group finder
            for (int i = 0; i < leftString.Length; i++)
                for (int j = 0; j < rightString.Length; j++)
                {
                    if(num[i,j] == 1)
                    {
                        if(!verticalIndexConstraints.Contains(i) && !horizontalIndexConstraints.Contains(j))
                        {
                            if (!valuableLinkedList.Contains(1))
                            {
                                valuableLinkedList.AddFirst(1);
                            }

                            valuableLinkedList.AddBefore(valuableLinkedList.Find(1), ((j + ((i * 10) + (j * 100) + (i * 1000) + 10000))));
                        }

                        if (!verticalIndexConstraints.Contains(i))
                            verticalIndexConstraints.Add(i);

                        if (!horizontalIndexConstraints.Contains(j))
                            horizontalIndexConstraints.Add(j);
                    }
                }

            //assign children to the father
            List<int> listSortingValues = new List<int>();
            List<int> linkedListToList = new List<int>();

            foreach (int item in valuableLinkedList)
            {
                linkedListToList.Add(item);

                if (item < 20)
                    listSortingValues.Add(item);
            }

            listSortingValues = InsertionSort(listSortingValues); //sort key numbers

            List<NodeBuilder> nodeList = new List<NodeBuilder>();
            int father = -1;

            for (int p = linkedListToList.Count - 1; p >= 0; p--)
            {
                if (linkedListToList[p] < 20)
                {
                    father = linkedListToList[p];
                }

                if (father != linkedListToList[p])
                    nodeList.Add(new NodeBuilder(father, linkedListToList[p]));
            }

            foreach (var item in listSortingValues)
            {
                foreach (NodeBuilder node in nodeList)
                {
                    if (node.GetFather == item)
                    {
                        returnList.Add(node.quantity);
                    }
                }
            }

            return returnList;
        }

        public List<int> InsertionSort(List<int> inputList)
        {
            for (int i = 0; i < inputList.Count - 1; i++)
            {
                for (int j = i + 1; j > 0; j--)
                {
                    if (inputList[j - 1] > inputList[j])
                    {
                        int temp = inputList[j - 1];
                        inputList[j - 1] = inputList[j];
                        inputList[j] = temp;
                    }
                }
            }

            inputList.Reverse();

            return inputList;
        }

        public int LongestCommonSubstring(string str1, string str2, out int[,] num)
        {
            num = new int[str1.Length, str2.Length];

            if (string.IsNullOrEmpty(str1) || string.IsNullOrEmpty(str2))
                return 0;

            int maxlen = 0;

            for (int i = 0; i < str1.Length; i++)
            {
                for (int j = 0; j < str2.Length; j++)
                {
                    if (str1[i] != str2[j])
                        num[i, j] = 0;
                    else
                    {
                        if ((i == 0) || (j == 0))
                            num[i, j] = 1;
                        else
                            num[i, j] = 1 + num[i - 1, j - 1];

                        if (num[i, j] > maxlen)
                        {
                            maxlen = num[i, j];
                        }
                    }
                }
            }
            return maxlen;
        }
    }
}

