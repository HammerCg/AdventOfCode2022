using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventCode2022
{
    class Day13
    {
        public void Execute()
        {
            string[] input = File.ReadAllLines("..\\..\\..\\Day13.txt");

            List<Pair> pairsPackets = new List<Pair>();
            for (int i = 0; i < input.Length; i++)
            {
                Pair currentPair = new Pair();
                currentPair.left = DecodeInput(input[i], 0, input[i].Length - 1);
                i++;
                currentPair.right = DecodeInput(input[i], 0, input[i].Length - 1);
                i++;
                pairsPackets.Add(currentPair);
            }

            //Part 1
            int rightCount = 0;
            for (int i = 0; i < pairsPackets.Count; i++)
            {
                if (ComparePackets(pairsPackets[i].left, pairsPackets[i].right) == 1)
                {
                    rightCount += i + 1;
                }
            }
            Console.WriteLine("Right order packets : " + rightCount);

            //Part 2
            Pair divisorsPair = new Pair();
            divisorsPair.left = DecodeInput("[[2]]", 0, 4);
            divisorsPair.left.divisorPacket = true;
            divisorsPair.right = DecodeInput("[[6]]", 0, 4);
            divisorsPair.right.divisorPacket = true;
            pairsPackets.Add(divisorsPair);
                                                        
            List<Packet> orderedPackets = new List<Packet>();

            for (int i = 0; i < pairsPackets.Count; i++)
            {
                orderedPackets.Add(pairsPackets[i].left);
                orderedPackets.Add(pairsPackets[i].right);
            }

            for (int i = 0; i < orderedPackets.Count - 1; i++) {               
                for (int j = 0; j < orderedPackets.Count - i - 1; j++) { 
                    if (ComparePackets(orderedPackets[j],orderedPackets[j + 1]) == -1)
                    {                          
                        Packet temp = orderedPackets[j];
                        orderedPackets[j] = orderedPackets[j + 1];
                        orderedPackets[j + 1] = temp;   
                    }
                }
            }

            int decoderKey = 1;
            for (int i = 0; i < orderedPackets.Count; i++)
            {                                                                        
                if (orderedPackets[i].divisorPacket)     
                    decoderKey *= (i + 1);            
            }
            Console.WriteLine("Decoder key : " + decoderKey);
        }
        int ComparePackets(Packet left , Packet right)
        {
            int n = 0;
            while(n < left.values.Count && n < right.values.Count)
            {
                if (left.values[n] is int && right.values[n] is int) 
                {
                    if ((int)left.values[n] > (int)right.values[n])             return -1;
                    else if ((int)left.values[n] < (int)right.values[n])        return 1;
                } 
                else if (left.values[n] is Packet || right.values[n] is Packet) 
                {
                    Packet leftPacket;
                    Packet rightPacket;
                    
                    if (left.values[n] is Packet)       leftPacket = (Packet)left.values[n];
                    else                                leftPacket = new Packet((int)left.values[n]);
                    if (right.values[n] is Packet)      rightPacket = (Packet)right.values[n];
                    else                                rightPacket = new Packet((int)right.values[n]);

                    int result = ComparePackets(leftPacket, rightPacket);
                    if (result == -1)           return -1;
                    else if (result == 1)       return 1;

                }
                n++;
            }

            if (left.values.Count > right.values.Count) return -1;
            else if (left.values.Count < right.values.Count) return 1;
            else return 0;
        }
        Packet DecodeInput(string line,int startIndex, int endIndex) {

            Packet packet = new Packet();
            line = line.Remove(endIndex, 1);
            line = line.Remove(0, 1);  
            for (int n = 0; n < line.Length; n++)
            {
                if (line[n] == '[') {
                    int indexClose = FindEndPacket( (string)line.Substring(n, line.Length- n));
                    Packet result = DecodeInput(line.Substring(n, indexClose+1), n, indexClose);   
                    packet.values.Add(result);
                    n = n+indexClose;
                }
                else if (Char.IsDigit(line[n])) {
                    
                    string c = line[n].ToString();
                    if (line.Length > (n + 1) && Char.IsDigit(line[n + 1]))
                    {
                        c += line[n + 1];
                        n++;
                    }
                    
                    packet.values.Add(int.Parse(c));
                }
                else if (line[n] == ']') { 
                    return packet; 
                }
            }
            return packet;        
        }
        int FindEndPacket(string line)
        {
            int opens = 0;
            int closeds = 0;
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == '[') opens++;
                if(line[i] == ']')
                {
                    closeds++;
                    if (opens == (closeds)) return i;
                }
            }
            return -1;
        }

        class Pair
        {
            public Packet left;
            public Packet right;
        }
        class Packet
        {
            public bool divisorPacket = false;                             
            public List<Object> values = new List<Object>();

            public Packet() { }
            public Packet(int value)
            {
                this.values.Add(value);
            }

        }
    }
}
