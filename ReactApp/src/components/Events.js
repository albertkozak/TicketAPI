import React, { Component } from "react";

class Events extends React.Component {
  constructor(props) {
    super();
    this.state = { events: [], loading: true };

    fetch(props.API_URL + "/events")
      .then(response => response.json())
      .then(data => {
        this.setState({
          events: data,
          loading: false
        });
      });
  }
  render() {
    let contents = this.state.loading ? (
      <p>
        <em>Loading...</em>
      </p>
    ) : (
      <table>
        <thead>
          <tr>
            <th>Event</th>
            <th>Venue</th>
          </tr>
        </thead>
        <tbody>
          {this.state.events.map(event => (
            <tr key={event.id}>
              <td>{event.eventName}</td>
              <td>{event.venueName}</td>
            </tr>
          ))}
        </tbody>
      </table>
    );
    return <div className="container">{contents}</div>;
  }
}

export default Events;
