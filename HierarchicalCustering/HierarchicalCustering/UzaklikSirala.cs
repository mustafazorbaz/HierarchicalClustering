using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HierarchicalCustering
{
    class UzaklikSirala
    {
        public void sirala(List<Data> dataSet)
        {

            for (int i = 0; i < dataSet.Count; i++)
            {
                for (int j = 0; j < dataSet.Count; j++)
                {
                    if (dataSet[i].getDistance() < dataSet[j].getDistance())
                    {
                        Data temp = dataSet[i];
                        dataSet[i] = dataSet[j];
                        dataSet[j] = temp;
                    }

                }
            }
          
        }
        public void sortCluster(List<Cluster> listCluster)
        {

            for (int i = 0; i < listCluster.Count; i++)
            {
                for (int j = 0; j < listCluster.Count; j++)
                {
                    if (listCluster[i].getClusteDistance() < listCluster[j].getClusteDistance())
                    {
                        Cluster temp = listCluster[i];
                        listCluster[i] = listCluster[j];
                        listCluster[j] = temp;
                    }

                }
            }

        }
    }
}
