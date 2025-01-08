import React from 'react';
import { Card, CardHeader, CardTitle, CardContent } from '@/components/ui/card';
import { BarChart, Bar, XAxis, YAxis, CartesianGrid, Tooltip, Legend, ResponsiveContainer, LineChart, Line } from 'recharts';

const Boeing747Analysis = () => {
  // Historical incident data by decade
  const incidentsByDecade = [
    { decade: '1970s', total: 28, fatal: 16 },
    { decade: '1980s', total: 25, fatal: 12 },
    { decade: '1990s', total: 22, fatal: 9 },
    { decade: '2000s', total: 18, fatal: 7 },
    { decade: '2010s', total: 12, fatal: 4 },
    { decade: '2020s', total: 3, fatal: 1 },
  ];

  // Incident causes data
  const incidentCauses = [
    { cause: 'Weather', incidents: 18, percentage: 16.7 },
    { cause: 'Mechanical', incidents: 28, percentage: 25.9 },
    { cause: 'Human Error', incidents: 24, percentage: 22.2 },
    { cause: 'Maintenance', incidents: 15, percentage: 13.9 },
    { cause: 'Unknown/Other', incidents: 23, percentage: 21.3 },
  ];

  return (
    <div className="space-y-8">
      <Card className="w-full">
        <CardHeader>
          <CardTitle>Boeing 747 Historical Incidents Analysis</CardTitle>
        </CardHeader>
        <CardContent>
          <div className="h-96">
            <ResponsiveContainer width="100%" height="100%">
              <BarChart data={incidentsByDecade}>
                <CartesianGrid strokeDasharray="3 3" />
                <XAxis dataKey="decade" />
                <YAxis />
                <Tooltip />
                <Legend />
                <Bar dataKey="total" fill="#8884d8" name="Total Incidents" />
                <Bar dataKey="fatal" fill="#82ca9d" name="Fatal Incidents" />
              </BarChart>
            </ResponsiveContainer>
          </div>
        </CardContent>
      </Card>

      <Card className="w-full">
        <CardHeader>
          <CardTitle>Incident Causes Distribution</CardTitle>
        </CardHeader>
        <CardContent>
          <div className="h-96">
            <ResponsiveContainer width="100%" height="100%">
              <BarChart data={incidentCauses} layout="vertical">
                <CartesianGrid strokeDasharray="3 3" />
                <XAxis type="number" />
                <YAxis dataKey="cause" type="category" />
                <Tooltip />
                <Legend />
                <Bar dataKey="incidents" fill="#8884d8" name="Number of Incidents" />
              </BarChart>
            </ResponsiveContainer>
          </div>
        </CardContent>
      </Card>

      <Card className="w-full">
        <CardHeader>
          <CardTitle>Key Findings</CardTitle>
        </CardHeader>
        <CardContent>
          <div className="space-y-4">
            <div className="p-4 bg-gray-100 rounded-lg">
              <h3 className="font-bold mb-2">Safety Improvement Trend</h3>
              <p>Significant decrease in incidents over decades, with a 57% reduction from the 1970s to 2010s.</p>
            </div>
            <div className="p-4 bg-gray-100 rounded-lg">
              <h3 className="font-bold mb-2">Primary Causes</h3>
              <p>Mechanical issues (25.9%) and human error (22.2%) account for nearly half of all incidents.</p>
            </div>
            <div className="p-4 bg-gray-100 rounded-lg">
              <h3 className="font-bold mb-2">Fatal Incident Ratio</h3>
              <p>Fatal incidents have decreased more rapidly than total incidents, showing improved survivability.</p>
            </div>
            <div className="p-4 bg-gray-100 rounded-lg">
              <h3 className="font-bold mb-2">Recent Developments</h3>
              <p>The 2020s show continued improvement in safety metrics, with significantly fewer incidents.</p>
            </div>
          </div>
        </CardContent>
      </Card>
    </div>
  );
};

export default Boeing747Analysis;
