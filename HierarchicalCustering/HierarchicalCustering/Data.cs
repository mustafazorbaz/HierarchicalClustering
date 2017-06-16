using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HierarchicalCustering
{
   
    public class Data
    {
        public List<double> values = new List<double>();

        private double distance;
        private string name;
        private int clusterId;
        private int parentClusterId;

      

        public void setName(string name)
        {
            this.name = name;

        }
        public string getName()
        {
            return name;

        }
        public void setDistance(double distance)
        {
            this.distance = distance;
        }
        public double getDistance()
        {
            return distance;
        }
        public void addValue(double value)
        {
            values.Add(value);

        }
        public double getValue(int indis)
        {
            return values[indis];

        }
      public int getClusterID()
        {
            return clusterId;
        }
        public void setClusterID(int clusterId)
        {
            this.clusterId = clusterId;
        }
        public int getParentClusterKumeId()
        {
            return parentClusterId;
        }

        public void setParentClusteKumeId(int parentClusteKumeId)
        {
            this.parentClusterId = parentClusteKumeId;
        }
    }
}
