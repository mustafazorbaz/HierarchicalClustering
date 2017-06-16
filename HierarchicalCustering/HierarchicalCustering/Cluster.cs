using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HierarchicalCustering
{
  public  class Cluster
    {
        public List<Data> cluster = new List<Data>();
        private double clusteDistance;
        private Cluster parentCluster;
        public double getClusteDistance()
        {
            return clusteDistance;
        }

        public void setClusteDistance(double clusteDistance)
        {
            this.clusteDistance = clusteDistance;
        }
        public Cluster getParentCluster()
        {
            return parentCluster;
        }

        public void setParentCluster(Cluster parentCluster)
        {
            this.parentCluster = parentCluster;
        }
        public List<Cluster> creatCluster(List<Data> dataSet, List<Cluster> listKume)
        {   

            for (int i = 0; i < dataSet.Count; i++)
            {
                Cluster cluster = new Cluster();
                cluster.cluster.Add(dataSet[i]);
                listKume.Add(cluster);
             //   listKume.Addcluster.Add(dataSet[i]);
            }
            return listKume;
        }
        public void showCluster(FormMain frm)
        {
            for (int i = 0; i < frm.listKume.Count; i++)
            {
                string members = "";
                for (int j = 0; j < frm.listKume[i].cluster.Count; j++)
                {
                    members += "  "+ frm.listKume[i].cluster[j].getName();
                  
                }
                frm.listBox2.Items.Add(members);
                members = "";
            }
          
        }
       
    }
}
